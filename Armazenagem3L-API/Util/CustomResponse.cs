using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public class CustomResponse {

        public HttpStatusCode StatusCode { get; set; }
        public CustomMessage Mensagem { get; set; }

        public CustomResponse(HttpStatusCode StatusCode, CustomMessage Mensagem) {
            this.StatusCode = StatusCode;
            this.Mensagem = Mensagem;
        }

    }
}
