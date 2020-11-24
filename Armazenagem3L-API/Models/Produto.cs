using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    public class Produto {

        public Produto() { }

        private int Id { get; set; }
        private string Nome { get; set; }
        private decimal Peso { get; set; }
        private decimal Preco { get; set; }

    }
}
