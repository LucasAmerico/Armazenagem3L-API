using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Armazenagem3L_API.Controllers {
    [Route("api/produto")]
    [ApiController]
    public class ProdutosController : ControllerBase {
        
        private readonly ProdutosService _service;
        private readonly ILoggerManager _logger;

        public ProdutosController(ProdutosService service, ILoggerManager logger) {
            _service = service;
            _logger = logger;
        }

        // GET api/produto/listagem?id=5
        [HttpGet("listagem")]
        public IActionResult Get(int id = 0) {
            var result = new object();

            if (id == 0) {
               result = _service.listagemProdutos();
            } else {
                result = _service.produtosById(id);
            }
            return Ok(result);
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto) {
          _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST Produto =>" + JsonSerializer.Serialize(produto));
          CustomResponse response = _service.Add(produto);
          return StatusCode((int)response.StatusCode, response.Mensagem);
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            var resultado = _service.DeletarProduto(id);
            return Ok(resultado);
        }
    }
}
