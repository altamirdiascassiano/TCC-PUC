using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace gisa.mic.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestadorController : Controller
    {
        [HttpGet]
        public IEnumerable<Prestador> Get()
        {
            var mock = new List<Prestador>() {
                new Prestador() {
                    Id= Guid.NewGuid().ToString(), Nome = "Prestador Mock 1", Descricao = "Prestador dsc Mock 1", DtCadastro = DateTime.UtcNow
                },
                new Prestador() {
                    Id= Guid.NewGuid().ToString(), Nome = "Prestador Mock 2", Descricao = "Prestador dsc Mock 2", DtCadastro = DateTime.UtcNow
                }
            };
            return mock;
        }

        [HttpPut]
        public ActionResult Put(Prestador prestador)
        {
            //if prestador is not valid
                // return bad request
            // dbService.Save (prestador)
            // eventBus.Add(prestador)

            return StatusCode(201);

        }

        [HttpPost]
        public ActionResult Post(Prestador prestador)
        {
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
            var prestadorMock = new Prestador() { Id = id, Nome = "Prestador Mock DB", Descricao = "DSC mock prestador db", DtCadastro = new DateTime(2022,01,01) }; //dbService busca pelo id
            if(prestadorMock != null)
                return prestadorMock;

            return prestadorMock;
        }

        [HttpDelete]
        public ActionResult Delete(Prestador prestador)
        {
            // delete db prestador
            return StatusCode(200);
        }
    }
}
