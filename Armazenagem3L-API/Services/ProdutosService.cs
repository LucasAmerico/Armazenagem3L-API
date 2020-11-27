using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Armazenagem3L_API.Services {
    public class ProdutosService {
        
        private readonly IProdutosRepository _repository;

        public ProdutosService(IProdutosRepository repository) {
            _repository = repository;
        }

        public Produto[] listagemProdutos() {

            Produto[] result = _repository.GetProdutos();

            return result;
        }

        public Produto produtosById(int id) {

            Produto result = _repository.GetProdutoById(id);

            return result;
        }
        
        public CustomMessage Add(Produto produto) {

            try {
                _repository.Add(produto);
                if (_repository.SaveChanges() == false) {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound){
                        Content = new StringContent(new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL).ToString()),
                    };
                    throw new HttpResponseException(response);
                }
                return new CustomMessage(Mensagens.SUCESSO, Mensagens.PRODUTO_ADD_SUCESSO);
            } catch (Exception) {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent(new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL).ToString()),
                };
                throw new HttpResponseException(response);
            }

        }

        public string DeletarProduto(int id) {
            Produto produto = _repository.GetProdutoById(id);
            if (produto == null) {
                return "Produto não encontrado";
            }
            _repository.Delete(produto);
            if (_repository.SaveChanges()) {
                return "Produto deletado com sucesso";
            }
            return "Não foi possível deletar o produto";
        }

    }
}
