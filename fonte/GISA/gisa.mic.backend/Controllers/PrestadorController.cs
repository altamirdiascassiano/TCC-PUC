using gisa.comum;
using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
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

        [HttpPut]
        public ActionResult Put(Prestador prestador)
        {
            Log.Debug("PrestadorController.cs -> Put()");
            _agenteFireBaseStorage.AdicionaDocumentoNaColecao<Prestador>(nameof(Prestador), prestador);
            return StatusCode(201);
        }

        [HttpPost]
        public ActionResult Post(string id,Prestador prestadorComAlteracao)
        {
            Log.Debug("PrestadorController.cs -> Post()");
            
            _agenteFireBaseStorage.AtualizaDocumentoNaColecao<Prestador>(nameof(Prestador), id, prestadorComAlteracao);
            return StatusCode(200);
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            Log.Debug("PrestadorController.cs -> Delete()");
            _agenteFireBaseStorage.RemoveDocumentoNaColecao<Prestador>(nameof(Prestador), id);            
            return StatusCode(200);
        }
    }
}
