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
        private readonly ProdutosService _service;

        public ProdutoController(ProdutosService service)
        {
            _service = service;
        }


        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var resultado = _service.DeletarProduto(id);
            return Ok(resultado);
        }
    }
}
