using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Services
{
    public class ProdutoService
    {
        private List<Produto> produtos = new List<Produto>() { 
            new Produto()
            {
                Id = 1,
                Nome = "Laranja",
                Peso = 20,
                Preco = 10
            },
            new Produto()
            {
                Id = 2,
                Nome = "Maça",
                Peso = 50,
                Preco = 25
            },
            new Produto()
            {
                Id = 3,
                Nome = "Abacaxi",
                Peso = 30,
                Preco = 15
            }
    };
    public List<Produto> listagemProdutos(int id) {

            if (id != null) { 
                //TODO Acessar o repository para buscar o produto especifico informado
            }

            return produtos;
        }
    }
}
