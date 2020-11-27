﻿using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;

namespace Armazenagem3L_API.Services {
    public class CargaService {
        private readonly ICargaRepository _repository;
        private readonly ILoggerManager _logger;
        private readonly IProdutosRepository _produto;


        public CargaService(ICargaRepository carga, IProdutosRepository produto, ILoggerManager logger) {
            _repository = carga;
            _produto = produto;
            _logger = logger;
        }

        public CustomResponse Add(Carga carga) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Add Carga =>" + JsonSerializer.Serialize(carga));
            ArrayList ProdutosAlterados = new ArrayList();
            try {
                using (var transaction = new TransactionScope()) {
                    _repository.Add(carga);

                    IEnumerable<ProdutoQtd> Ids = carga.Produtos;

                    foreach (var item in Ids) {
                        Produto p = _produto.GetProdutoById(item.ProdutoId);
                        if (p == null) {
                            return new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_BUSCA_PRODUTO));
                        } else {
                            if (item.Qtd > p.Qtd) {
                                return new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_SALVAR_CARGA));
                            } else {
                                p.Qtd -= item.Qtd;
                                ProdutosAlterados.Add(p);
                            }
                        }

                    }

                    if(_repository.SaveChanges() == true) {
                        AtualizaProdutos(ProdutosAlterados);
                        InsereCargaProdutos(carga.Produtos, _repository.GetLast().Id);
                        transaction.Complete();
                    } else {
                        transaction.Dispose();
                        return new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_ADD_FALHA));
                    }
                    
                }

                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ADD_SUCESSO));
            } catch (HttpResponseException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Carga =>" + JsonSerializer.Serialize(ex.InnerException));
                return new CustomResponse(HttpStatusCode.InternalServerError, new CustomMessage(Mensagens.ERRO, JsonSerializer.Serialize(ex.InnerException)));
            }
        }

        public CustomResponse AceitarCarga(MotoristaCarga carga) {
            _logger.LogDebug("[INFO] Executando funcao (Service): AceitarCarga Carga =>" + JsonSerializer.Serialize(carga));

            try {
                var CargaEscolhida = _repository.FindById(carga.CargaId);

                CargaEscolhida.MotoristaId = carga.MotoristaId;
                _repository.Update(CargaEscolhida);
                if(_repository.SaveChanges() == false) {
                    return new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_ACEITA_ERRO));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ACEITA));
            } catch(HttpResponseException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): AceitarCarga Carga =>" + JsonSerializer.Serialize(ex.InnerException));
                return new CustomResponse(HttpStatusCode.InternalServerError, new CustomMessage(Mensagens.ERRO, JsonSerializer.Serialize(ex.InnerException)));
            }
        }

        private void InsereCargaProdutos(IEnumerable<ProdutoQtd> produtos, int id) {
            ArrayList Cargas = new ArrayList();
            foreach (var item in produtos) {
                _repository.AddCargaProdutos(new CargaProduto(id, item.ProdutoId, item.Qtd));
                _repository.SaveChanges();
            }

        }

        public void AtualizaProdutos(ArrayList produtos) {
            foreach (var item in produtos) {
                _produto.Update((Produto)item);
            }
        } 

    }
}
