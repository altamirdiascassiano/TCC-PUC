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
    public class AssociadoController : Controller
    {
        AgenteFireBaseStorage _agenteFireBaseStorage;
        AgenteSQS _agenteSQS;
        public AssociadoController()
        {
            _agenteFireBaseStorage = new AgenteFireBaseStorage();
            _agenteSQS = new AgenteSQS();
        }

        /// <summary>
        /// Busca todos os Associadoes cadastrados
        /// </summary>        
        /// <returns>Todos Associadoes</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        [HttpGet]
        public async Task<IEnumerable<Associado>> Get()
        {            
            Log.Debug("Iniciando consulta a base do Firebase");
            var documentos = await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Associado>(nameof(Associado));
            Log.Debug("Consulta a base do Firebase finalizada");
            var Associadoes = new List<Associado>();
            Log.Debug("AssociadoController.cs -> Get()");
            return documentos;
        }

        /// <summary>
        /// Busca o Associado pelo seu Id
        /// </summary>
        /// <param name="id">Valor do Identificado no FireBase</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="204">Fornecedor não identificado</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<Associado> Get(string id)
        {
            Log.Debug("Iniciando consulta a base do Firebase no id " + id);
            var documento = await _agenteFireBaseStorage.BuscaDocumentoColecaoPorIdAsync<Associado>(nameof(Associado), id);
            return documento;
        }
        
        /// <summary>
        /// Salva novo Associado
        /// </summary>
        /// <param name="Associado">Recebe Associado que será gravado na base</param>
        /// <response code="201">Gravação efetuada com sucesso</response>
        [HttpPost]
        public async Task<ActionResult> Post(Associado Associado)
        {
            Log.Debug("AssociadoController.cs -> Post()");
            _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Associado>(nameof(Associado), Associado);
            _agenteSQS.SalvaNoEventBus(JsonSerializer.Serialize(Associado));
            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza o Associado
        /// </summary>
        /// <param name="id">Identificador do Associado</param>
        /// /// <param name="AssociadoComAlteracao">Associado com os valores que será atualizado</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpPut]
        public async Task<ActionResult> Put(string id, Associado AssociadoComAlteracao)
        {
            Log.Debug("AssociadoController.cs -> Put()");
            await _agenteSQS.SalvaNoEventBus(JsonSerializer.Serialize(AssociadoComAlteracao));
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Associado>(nameof(Associado), id, AssociadoComAlteracao);
            return StatusCode(200);
        }

        /// <summary>
        /// Remove o Associado
        /// </summary>
        /// <param name="id">Identificador do Associado</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            Log.Debug("AssociadoController.cs -> Delete()");
            Log.Information("Iniciado deleção do fornecedor " + id);
            _agenteFireBaseStorage.RemoveDocumentoNaColecao<Associado>(nameof(Associado), id);
            return StatusCode(200);
        }
    }
}
