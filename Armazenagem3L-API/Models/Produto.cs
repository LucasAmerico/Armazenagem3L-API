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

        public Produto(int id, string nome, decimal peso, decimal preco, int qtd) {
            Id = id;
            Nome = nome;
            Peso = peso;
            Preco = preco;
            Qtd = qtd;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Peso { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [Required]
        [Range(1, 999)]
        public int Qtd { get; set; }

    }
}
