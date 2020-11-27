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

        public CustomResponse Add(Produto produto) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Add Produto =>" + JsonSerializer.Serialize(produto));
            try {
                _repository.Add(produto);
                if (_repository.SaveChanges() == false) {
                    return new CustomResponse(HttpStatusCode.BadRequest, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.PRODUTO_ADD_SUCESSO));
            } catch (HttpResponseException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Produto =>" + JsonSerializer.Serialize(ex.InnerException));
                return new CustomResponse(HttpStatusCode.InternalServerError, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL));
            }

        }


    }
}
