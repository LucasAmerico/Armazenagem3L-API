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

        // GET: api/<ProdutosController>
        [HttpGet]
        public IEnumerable<Funcionario> Get() {
            return null;
        }

        // GET api/<ProdutosController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto) {
          _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST Produto =>" + JsonSerializer.Serialize(produto));
          CustomResponse response = _service.Add(produto);
          return StatusCode((int)response.StatusCode, response.Mensagem);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
