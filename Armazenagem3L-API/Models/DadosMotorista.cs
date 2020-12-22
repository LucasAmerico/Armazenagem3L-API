using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    public class DadosMotorista {

        public string senha { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }


        public DadosMotorista() {
        }

        public DadosMotorista(string senha, string email, string login) {
            this.senha = senha;
            Email = email;
            Login = login;
        }
    }
}
