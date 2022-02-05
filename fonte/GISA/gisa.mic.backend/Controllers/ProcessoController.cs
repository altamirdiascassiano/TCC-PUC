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
        public ProcessoController()
        {
            _agenteFireBaseStorage = new AgenteFireBaseStorage();
            _agenteSQS = new AgenteSQS();
        }

        /// <summary>
        /// Busca todos os Processos cadastrados
        /// </summary>        
        /// <returns>Todos Processos</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        [HttpGet]
        public async Task<IEnumerable<Processo>> Get()
        {
            Log.Debug("Iniciando consulta a base do Firebase");
            var documentos = await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Processo>(nameof(Processo));
            Log.Debug("Consulta a base do Firebase finalizada");
            var Processos = new List<Processo>();
            Log.Debug("ProcessoController.cs -> Get()");
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
            Log.Debug("ProcessoController.cs -> Post()");
            _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Processo>(nameof(Processo), Processo);
            _agenteSQS.SalvaNoEventBus(JsonSerializer.Serialize(Processo));
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
        public ActionResult Put(string id, Processo ProcessoComAlteracao)
        {
            Log.Debug("ProcessoController.cs -> Put()");
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Processo>(nameof(Processo), id, ProcessoComAlteracao);
            _agenteSQS.SalvaNoEventBus(JsonSerializer.Serialize(ProcessoComAlteracao));
            return StatusCode(200);
        }

        /// <summary>
        /// Remove o Processo
        /// </summary>
        /// <param name="id">Identificador do Processo</param>
        /// <returns>Um único Processo</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            Log.Debug("ProcessoController.cs -> Delete()");
            Log.Information("Iniciado deleção do fornecedor " + id);
            _agenteFireBaseStorage.RemoveDocumentoNaColecao<Processo>(nameof(Processo), id);
            return StatusCode(200);
        }
    }
}
