using Armazenagem3L_API.ExceptionHandler;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Util;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;

namespace Armazenagem3L_API.Services {
    public class ProdutosService {
        
        private readonly IProdutosRepository _repository;
        private readonly ILoggerManager _logger;

        public ProdutosService(IProdutosRepository repository, ILoggerManager logger) {
            _repository = repository;
            _logger = logger;
        }

        public Produto[] listagemProdutos() {
            _logger.LogDebug("[INFO] Executando funcao (Service): listagemProdutos");

            Produto[] result = _repository.GetProdutos();

            return result;
        }

        public Produto produtosById(int id) {
            _logger.LogDebug("[INFO] Executando funcao (Service): ProdutosById  Produto =>" + JsonSerializer.Serialize(id));

            Produto result = _repository.GetProdutoById(id);

            return result;
        }
        
        public CustomResponse Add(Produto produto) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Add Produto =>" + JsonSerializer.Serialize(produto));
            try {
                _repository.Add(produto);
                if (_repository.SaveChanges() == false) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_GERAL);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.PRODUTO_ADD_SUCESSO), null);
            } catch (HttpResponseException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Produto =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse DeletarProduto(int id) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Deletar Produto =>" + JsonSerializer.Serialize(id));
            try {
                Produto produto = _repository.GetProdutoById(id);
                if (produto == null) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.PRODUTO_NAO_ENCONTRADO);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }
                _repository.Delete(produto);
                if (_repository.SaveChanges() == false) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_DELETAR_PRODUTO);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);
            } catch (HttpResponseException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Produto =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

    }
}
