using gisa.comum;
using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
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

        public AssociadoController(AgenteSQS agenteSQS, AgenteFireBaseStorage agenteFireBaseStorage)
        {
            _agenteFireBaseStorage = agenteFireBaseStorage;
            _agenteSQS = agenteSQS;
        }

        /// <summary>
        /// Busca todos os Associadoes cadastrados
        /// </summary>        
        /// <returns>Todos Associadoes</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Associado>>> Get()
        {
            try
            {
                Log.Debug("Iniciando consulta a base do Firebase");
                var associados = await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Associado>(nameof(Associado));
                Log.Debug("Consulta a base do Firebase finalizada");
                return associados;
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca o Associado pelo seu Id
        /// </summary>
        /// <param name="id">Valor do Identificado no FireBase</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="204">Fornecedor não identificado</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Associado>> Get(string id)
        {
            try
            {
                Log.Debug("Iniciando consulta a base do Firebase no id " + id);
                var associado = await _agenteFireBaseStorage.BuscaDocumentoColecaoPorIdAsync<Associado>(nameof(Associado), id);
                return associado;
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Salva novo Associado
        /// </summary>
        /// <param name="Associado">Recebe Associado que será gravado na base</param>
        /// <response code="201">Gravação efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpPost]
        public async Task<ActionResult> Post(Associado Associado)
        {
            try
            {
                var jsonAssociado = JsonSerializer.Serialize(Associado);
                Log.Debug("Gravando associado na base do Firebase = " + jsonAssociado);
                _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Associado>(nameof(Associado), Associado);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                _agenteSQS.SalvaNoEventBus(jsonAssociado);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza o Associado
        /// </summary>
        /// <param name="id">Identificador do Associado</param>
        /// /// <param name="AssociadoComAlteracao">Associado com os valores que será atualizado</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(string id, Associado AssociadoComAlteracao)
        {
            try
            {
                var jsonAssociado = JsonSerializer.Serialize(AssociadoComAlteracao);
                Log.Debug("Atualizando associado com o Id= " + id + " para o associado " + jsonAssociado);
                _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Associado>(nameof(Associado), id, AssociadoComAlteracao);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                _agenteSQS.SalvaNoEventBus(jsonAssociado);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remove o Associado
        /// </summary>
        /// <param name="id">Identificador do Associado</param>
        /// <returns>Um único Associado</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                Log.Information("Iniciado deleção do fornecedor no Firebase Storage" + id);
                _agenteFireBaseStorage.RemoveDocumentoNaColecao<Associado>(nameof(Associado), id);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                _agenteSQS.SalvaNoEventBus(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }
    }
}
