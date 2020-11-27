using Armazenagem3L_API.Data;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Armazenagem3L_API.Controllers {
    [Route("api/produto")]
    [ApiController]
    public class ProdutosController : ControllerBase {
        
        private readonly ProdutosService _service;

        public ProdutosController(ProdutosService service) {
            _service = service;
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
        public ActionResult<CustomMessage> Post([FromBody] Produto produto) {
          CustomMessage message = _service.Add(produto);
            return Ok(message);
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
