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
    public class PrestadorController : Controller
    {
        AgenteFireBaseStorage _agenteFireBaseStorage;
        AgenteSQS _agenteSQS;
        public PrestadorController(AgenteSQS agenteSQS, AgenteFireBaseStorage agenteFireBaseStorage)
        {
            _agenteFireBaseStorage = agenteFireBaseStorage;
            _agenteSQS = agenteSQS;
        }

        /// <summary>
        /// Busca todos os prestadores cadastrados
        /// </summary>        
        /// <returns>Todos prestadores</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestador>>> Get()
        {
            try
            {
                Log.Debug("Iniciando consulta a base do Firebase");
                var prestadores = await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Prestador>(nameof(Prestador));
                Log.Debug("Consulta a base do Firebase finalizada");                
                return prestadores;
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca o prestador pelo seu Id
        /// </summary>
        /// <param name="id">Valor do Identificado no FireBase</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="204">Fornecedor não identificado</response>
        /// /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Prestador>> Get(string id)
        {
            try
            {
                Log.Debug("Iniciando consulta a base do Firebase no id " + id);
                var documento = await _agenteFireBaseStorage.BuscaDocumentoColecaoPorIdAsync<Prestador>(nameof(Prestador), id);
                return documento;
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Salva novo prestador
        /// </summary>
        /// <param name="prestador">Recebe prestador que será gravado na base</param>
        /// <response code="201">Gravação efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpPost]
        public async Task<ActionResult> Post(Prestador prestador)
        {
            try
            {
                var jsonPrestador = JsonSerializer.Serialize(prestador);
                Log.Debug("Gravando associado na base do Firebase = " + jsonPrestador);
                await _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Prestador>(nameof(Associado), prestador);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                await _agenteSQS.SalvaNoEventBus(jsonPrestador);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza o prestador
        /// </summary>
        /// <param name="id">Identificador do prestador</param>
        /// /// <param name="prestadorComAlteracao">Prestador com os valores que será atualizado</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(string id, Prestador prestadorComAlteracao)
        {
            try
            {
                var jsonPrestador = JsonSerializer.Serialize(prestadorComAlteracao);
                Log.Debug("Atualizando associado com o Id= " + id + " para o prestador " + jsonPrestador);
                await _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Prestador>(nameof(Prestador), id, prestadorComAlteracao);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                await _agenteSQS.SalvaNoEventBus(jsonPrestador);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Mensagem: ", ex.Message, " StackTrace: ", ex.StackTrace));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remove o prestador
        /// </summary>
        /// <param name="id">Identificador do prestador</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        /// <response code="500">Ocorreu um erro interno que não foi possível realizar a operação</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            try
            {
                Log.Information("Iniciado deleção do prestador no Firebase Storage" + id);
                await _agenteFireBaseStorage.RemoveDocumentoNaColecao<Prestador>(nameof(Prestador), id);
                Log.Debug("Comunicando com o Event Bus Amazon SQS");
                await _agenteSQS.SalvaNoEventBus(id);
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
