﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Motoristas", Schema = "Armazenagem3L")]
    public class Motorista {

        public Motorista() { }
        public Motorista(int Id, String Nome) {
            this.Id = Id;
            this.Nome = Nome;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
