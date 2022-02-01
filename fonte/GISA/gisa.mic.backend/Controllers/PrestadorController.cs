using gisa.comum;
using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gisa.mic.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestadorController : Controller
    {
        AgenteFireBaseStorage _agenteFireBaseStorage;
        public PrestadorController()
        {
            _agenteFireBaseStorage = new AgenteFireBaseStorage();
        }
        
        /// <summary>
        /// Busca todos os prestadores cadastrados
        /// </summary>        
        /// <returns>Todos prestadores</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        [HttpGet]
        public async Task<IEnumerable<Prestador>> Get()
        {           
            Log.Debug("Iniciando consulta a base do Firebase");           
            var documentos= await _agenteFireBaseStorage.BuscaTodosDocumentosColecaoAsync<Prestador>(nameof(Prestador));
            Log.Debug("Consulta a base do Firebase finalizada");
            var prestadores = new List<Prestador>();
            Log.Debug("PrestadorController.cs -> Get()");   
            return documentos;
        }

        /// <summary>
        /// Busca o prestador pelo seu Id
        /// </summary>
        /// <param name="id">Valor do Identificado no FireBase</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Busca efetuada com sucesso</response>
        /// <response code="204">Fornecedor não identificado</response>
        [HttpGet]
        [Route("/{id}")]
        public async Task<Prestador> Get(string id)
        {
            Log.Debug("Iniciando consulta a base do Firebase no id " + id);
            var documento = await _agenteFireBaseStorage.BuscaDocumentoColecaoPorIdAsync<Prestador>(nameof(Prestador), id);            
            return documento;
        }

        /// <summary>
        /// Salva novo prestador
        /// </summary>
        /// <param name="prestador">Recebe prestador que será gravado na base</param>
        /// <response code="201">Gravação efetuada com sucesso</response>
        [HttpPost]
        public ActionResult Post(Prestador prestador)
        {
            Log.Debug("PrestadorController.cs -> Post()");
            _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Prestador>(nameof(Prestador), prestador);
            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza o prestador
        /// </summary>
        /// <param name="id">Identificador do prestador</param>
        /// /// <param name="prestadorComAlteracao">Prestador com os valores que será atualizado</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpPut]
        public ActionResult Put(string id,Prestador prestadorComAlteracao)
        {
            Log.Debug("PrestadorController.cs -> Put()");
            
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Prestador>(nameof(Prestador), id, prestadorComAlteracao);
            return StatusCode(200);
        }

        /// <summary>
        /// Remove o prestador
        /// </summary>
        /// <param name="id">Identificador do prestador</param>
        /// <returns>Um único prestador</returns>
        /// <response code="200">Atualização efetuada com sucesso</response>
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            Log.Debug("PrestadorController.cs -> Delete()");
            Log.Information("Iniciado deleção do fornecedor " + id);
            _agenteFireBaseStorage.RemoveDocumentoNaColecao<Prestador>(nameof(Prestador), id);            
            return StatusCode(200);
        }
    }
}
