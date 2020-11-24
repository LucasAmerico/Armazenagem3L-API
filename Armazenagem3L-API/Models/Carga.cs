using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    public class Carga {

        public Carga() { }

        private int Id { get; set; }
        private List<Produto> produtos { get; set; } 
        private string Endereco { get; set; }
        private decimal Frete { get; set; }
        private int MotoristaId { get; set; }

    }
}
