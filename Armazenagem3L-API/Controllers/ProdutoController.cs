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
    public class ProdutoController : ControllerBase
    {
        private ProdutoService service = new ProdutoService();

        // GET api/produto/listagem?id=5
        [HttpGet("listagem")]
        public IActionResult Listagem(int id)
        {
            List<Produto> lista = service.listagemProdutos(id);
            return Ok(lista);
        }

    }
}
