using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.ExceptionHandler {
    public class ApiCustomException : Exception {

        public ApiCustomException() {

        }
        public ApiCustomException(string name): base(name) {

        }

    }
}
