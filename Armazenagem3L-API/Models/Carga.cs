using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Cargas", Schema = "Armazenagem3L")]
    public class Carga {

        public Carga() { }
        [Key]
        public int Id { get; set; }
        public List<Produto> Produtos { get; set; }
        public string Endereco { get; set; }
        public decimal Frete { get; set; }
        [Required]
        public virtual Motorista Motorista { get; set; }
        public virtual int MotoristaId { get; set; }

    }
}
