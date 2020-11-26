using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Services
{
    public class ProdutosService
    {

        private readonly IProdutosRepository _repository;

        public ProdutosService(IProdutosRepository repository)
        {
            _repository = repository;
        }

        public Produto[] listagemProdutos() {

            Produto[] result = _repository.GetProdutos();

            return result;
        }

        public Produto produtosById(int id)
        {

            Produto result = _repository.GetProdutoById(id);

            return result;
        }
    }
}
