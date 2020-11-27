using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public static class Mensagens {
        public const string SUCESSO = "Sucesso";
        public const string ERRO = "Erro";
        public const string PRODUTO_ADD_SUCESSO = "Produto criado com sucesso!";
        public const string CARGA_ADD_SUCESSO = "Carga Adicionada com sucesso!";
        public const string CARGA_ADD_FALHA = "Erro no processo de criação de carga!";
        public const string CARGA_ACEITA = "A Carga foi atribuida com sucesso ao Motorista!";
        public const string CARGA_ACEITA_ERRO = "Não foi possivel atribuir a carga ao Motorista!";
        public const string ERRO_GERAL = "Não foi possível realizar a operação desejada, tente novamente mais tarde!";
        public const string ERRO_BUSCA_PRODUTO = "Não foi possível realizar a operação desejada, um produto da lista não existe!";
        public const string ERRO_SALVAR_CARGA = "A quatidade de um produto excede seu estoque!";
        public const string PRODUTO_NAO_ENCONTRADO = "Produto não encontrado";
        public const string DELETAR_PRODUTO = "Produto deletado com sucesso";
        public const string ERRO_DELETAR_PRODUTO = "Não foi possível deletar o produto";
    }
}