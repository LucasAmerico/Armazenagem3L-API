using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Produtos", Schema = "Armazenagem3L")]
    public class Produto {

        public Produto() { }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public decimal Preco { get; set; }

    }
}
