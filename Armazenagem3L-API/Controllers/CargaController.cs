using Armazenagem3L_API.Logger;
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
    [Route("api/carga")]
    [ApiController]
    public class CargaController : ControllerBase {

        private readonly ILoggerManager _logger;
        private readonly CargaService _service;

        public CargaController(ILoggerManager logger, CargaService service) {
            _logger = logger;
            _service = service;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/carga/listagem?id=5&motorista=1
        [HttpGet("listagem")]
        public IActionResult Get(int id = 0, int motorista = 0)
        {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): GET Carga id =>" + JsonSerializer.Serialize(id));

            var result = new object();

            if (id == 0 && motorista == 0)
            {
                result = _service.listagemCargas();
            }
            if (id > 0 && motorista == 0)
            {
                result = _service.cargaById(id);
            }
            if (id == 0 && motorista > 0)
            {
                result = _service.cargaByMotoristaId(motorista);
            }
            else if (id > 0 && motorista > 0)
            {
                result = _service.cargaByIdAndMotoristaId(id, motorista);
            }
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Carga value) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST Carga =>" + JsonSerializer.Serialize(value));
            CustomResponse response = _service.Add(value);

            return StatusCode((int)response.StatusCode, response.Mensagem);
        }

        // POST api/aceitarCarga
        [HttpPost("aceitarCarga")]
        public IActionResult AceitarCarga([FromBody] MotoristaCarga value) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST AceitarCarga =>" + JsonSerializer.Serialize(value));
            CustomResponse response = _service.AceitarCarga(value);

            return StatusCode((int)response.StatusCode, response.Mensagem);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): DELETE Carga id =>" + JsonSerializer.Serialize(id));
            CustomResponse response = _service.DeletarCarga(id);
            return StatusCode((int)response.StatusCode, response.Mensagem);
        }
    }
}
