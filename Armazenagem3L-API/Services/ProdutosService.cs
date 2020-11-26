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

        public string DeletarProduto(int id)
        {
            Produto produto = _repository.GetProdutoById(id);
            if (produto == null) {
                return "Produto não encontrado";
            }
            _repository.Delete(produto);
            if (_repository.SaveChanges())
            {
                return "Produto deletado com sucesso";
            }
            return "Não foi possível deletar o produto";
        }
    }
}
