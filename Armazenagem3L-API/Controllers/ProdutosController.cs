using Armazenagem3L_API.Models;
using Armazenagem3L_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Armazenagem3L_API.Controllers
{

    [Route("api/produto")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly ProdutosService _service;

        public ProdutosController(ProdutosService service)
        {
            _service = service;
        }
        // GET api/produto/listagem?id=5
        [HttpGet("listagem")]
        public IActionResult Listagem(int id = 0)
        {
            var result = new object();

            if (id == 0) {
               result = _service.listagemProdutos();
            } else
            {
                result = _service.produtosById(id);
            }
            return Ok(result);
        }

    }
}
