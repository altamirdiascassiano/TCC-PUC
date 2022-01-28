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
        public ActionResult Post(Prestador prestador)
        {
            Log.Debug("PrestadorController.cs -> Post()");
            Prestador prestadorRecuperado = GetPrestadorDB(prestador.Id);
            if(prestadorRecuperado != null)
                //db.update(prestadorRecuperado, prestador)
                return StatusCode(200);
            //if prestador is not valid
            // return bad request
            // dbService.Save (prestador)
            // eventBus.Add(prestador)

            return StatusCode(200);

        }
        private Prestador GetPrestadorDB(string id)
        {
            Log.Debug("PrestadorController.cs -> GetPrestadorDB()");
            var prestadorMock = new Prestador() { Id = id, Nome = "Prestador Mock DB", Descricao = "DSC mock prestador db", DtCadastro = new DateTime(2022,01,01) }; //dbService busca pelo id
            if(prestadorMock != null)
                return prestadorMock;

            return prestadorMock;
        }

        [HttpDelete]
        public ActionResult Delete(Prestador prestador)
        {
            Log.Debug("PrestadorController.cs -> Delete()");
            // delete db prestador
            return StatusCode(200);
        }
    }
}
