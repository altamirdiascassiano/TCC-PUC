using gisa.mic.backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace gisa.mic.backend.Controllers
{
    public class PrestadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Prestador> Get()
        {
            var mock = new List<Prestador>() { 
                new Prestador() { 
                    Nome = "Prestador Mock 1", Descricao = "Prestador dsc Mock 1", DtCadastro = DateTime.UtcNow 
                },
                new Prestador() {
                    Nome = "Prestador Mock 2", Descricao = "Prestador dsc Mock 2", DtCadastro = DateTime.UtcNow
                }
            };
            return mock;
        }
    }
}
