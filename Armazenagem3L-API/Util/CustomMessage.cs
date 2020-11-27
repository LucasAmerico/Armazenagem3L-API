using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public class CustomMessage {
        public string Nome;
        public string Descricao;

        public CustomMessage(string Nome, string Descricao) {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }
    }
}
