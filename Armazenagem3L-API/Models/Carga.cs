﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Cargas", Schema = "Armazenagem3L")]
    public class Carga {

        public Carga() { }

        public Carga(string endereco, decimal frete, int motoristaId, IEnumerable<ProdutoQtd> produtos) {
            Endereco = endereco;
            Frete = frete;
            MotoristaId = motoristaId;
            Produtos = produtos;
        }

        [Key]
        public int Id { get; set; }
        public string Endereco { get; set; }
        public decimal Frete { get; set; }
        public int MotoristaId { get; set; }

        [NotMapped]
        public IEnumerable<ProdutoQtd> Produtos { get; set; }

        [NotMapped]
        public Motorista Motorista { get; set; }
    }
}
