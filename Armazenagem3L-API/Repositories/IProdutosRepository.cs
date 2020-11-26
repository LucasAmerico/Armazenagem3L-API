using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories
{
    public interface IProdutosRepository {

        void Delete(Produto produto);
        Produto GetProdutoById(int produtoId);
        bool SaveChanges();
    }
}
