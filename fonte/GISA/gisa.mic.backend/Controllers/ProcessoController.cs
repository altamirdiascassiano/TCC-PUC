using gisa.comum;
using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace gisa.mic.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessoController : Controller
    {
        AgenteFireBaseStorage _agenteFireBaseStorage;
        AgenteSQS _agenteSQS;
        public ProcessoController(AgenteSQS agenteSQS, AgenteFireBaseStorage agenteFireBaseStorage)
        {            
            _agenteFireBaseStorage = agenteFireBaseStorage;
            _agenteSQS = agenteSQS;
        }

        /// <summary>
        /// Busca todos os Processos cadastrados
        /// </summary>        
        /// <returns>Todos Processos</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        [HttpGet]
        public async Task<IEnumerable<Processo>> Get()
        {
            Log.Debug("Iniciando consulta a base do Firebase storage");
            var documentos = await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Processo>(nameof(Processo));
            var Processos = new List<Processo>();            
            return documentos;
        }

        /// <summary>
        /// Busca o Processo pelo seu Id
        /// </summary>
        /// <param name="id">Valor do Identificado no FireBase</param>
        /// <returns>Um único Processo</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="204">Fornecedor não identificado</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<Processo> Get(string id)
        {
            Log.Debug("Iniciando consulta a base do Firebase no id " + id);
            var documento = await _agenteFireBaseStorage.BuscaDocumentoColecaoPorIdAsync<Processo>(nameof(Processo), id);
            Log.Debug("Consulta a base do Firebase finalizada");
            return documento;
        }

        /// <summary>
        /// Salva novo Processo
        /// </summary>
        /// <param name="Processo">Recebe Processo que será gravado na base</param>
        /// <response code="201">Gravação efetuada com sucesso</response>
        [HttpPost]
        public ActionResult Post(Processo Processo)
        {
            var jsonProcesso = JsonSerializer.Serialize(Processo);
            Log.Debug("Gravando processo na base do Firebase = " + jsonProcesso);
            _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Processo>(nameof(Processo), Processo);
            Log.Debug("Comunicando com o Event Bus Amazon SQS");
            _agenteSQS.SalvaNoEventBus(jsonProcesso);
            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza o Processo
        /// </summary>
        /// <param name="id">Identificador do Processo</param>
        /// /// <param name="ProcessoComAlteracao">Processo com os valores que será atualizado</param>
        /// <returns>Um único Processo</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(string id, Processo ProcessoComAlteracao)
        {
            Log.Debug("ProcessoController.cs -> Put()");
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Processo>(nameof(Processo), id, ProcessoComAlteracao);
            _agenteSQS.SalvaNoEventBus(JsonSerializer.Serialize(ProcessoComAlteracao));
            return StatusCode(200);


            var jsonProcesso = JsonSerializer.Serialize(ProcessoComAlteracao);
            Log.Debug("Atualizando processo com o Id= " + id + " para o processo " + jsonProcesso);
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Processo>(nameof(Processo), id, ProcessoComAlteracao);
            Log.Debug("Comunicando com o Event Bus Amazon SQS");
            _agenteSQS.SalvaNoEventBus(jsonProcesso);
            return StatusCode(200);
        }

        /// <summary>
        /// Remove o Processo
        /// </summary>
        /// <param name="id">Identificador do Processo</param>
        /// <returns>Um único Processo</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(string id)
        {
            Log.Information("Iniciado deleção do processo no Firebase Storage" + id);
            _agenteFireBaseStorage.RemoveDocumentoNaColecao<Processo>(nameof(Processo), id);
            Log.Debug("Comunicando com o Event Bus Amazon SQS");
            _agenteSQS.SalvaNoEventBus(id);
            return StatusCode(200);
        }
    }
}
