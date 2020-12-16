using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Moq;
using System;
using System.Net;
using Xunit;

namespace Armazenagem_3LC__Tests {
    public class ProdutoServiceTest {
        private const int Id = 1;
        private const string Nome = "Produto";
        private const int Peso = 3200;
        private const int Preco = 3400;
        private const int Qtd = 300;
        private const int ID_PARAMETRO = 1;
        Mock<IProdutosRepository> _repository = new Mock<IProdutosRepository>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();

        [Fact]
        public void add_requisicaoCorreta_sucesso() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.PRODUTO_ADD_SUCESSO), null);

            _repository.Setup(m => m.Add(It.IsAny<Produto>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockProduto);
            

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Add(It.IsAny<Produto>()), Times.Exactly(1));

        }

        [Fact]
        public void add_requisicaoIncorreta_erro() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL), null);

            _repository.Setup(m => m.Add(It.IsAny<Produto>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockProduto);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Add(It.IsAny<Produto>()), Times.Exactly(1));
        }

        [Fact]
        public void DeletarProduto_requisicaoCorreta_sucesso() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

            _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);
            _repository.Setup(m => m.Delete(It.IsAny<Produto>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.Delete(It.IsAny<Produto>()), Times.Exactly(1));

        }

        [Fact]
        public void DeletarProduto_ProdutoNaoEncontrado_erro() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.PRODUTO_NAO_ENCONTRADO), null);

            _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns((Produto)null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void DeletarProduto_ErroBancoDeletarProduto_Erro() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_DELETAR_PRODUTO), null);

            _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);
            _repository.Setup(m => m.Delete(It.IsAny<Produto>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.Delete(It.IsAny<Produto>()), Times.Exactly(1));

        }

        [Fact]
        public void ProdutosById_requisicaoCorreta_sucesso() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

            _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.produtosById(ID_PARAMETRO);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void ListagemProdutos_requisicaoCorreta_sucesso() {
            //PREPARACAO
            ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
            Produto mockProduto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

            _repository.Setup(m => m.GetProdutos()).Verifiable();

            //EXECUÇÂO
            CustomResponse actualResponse = _service.listagemProdutos();

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.GetProdutos(), Times.Exactly(1));

        }


        public Produto MockProduto() {
            return new Produto(Id, Nome, Peso, Preco, Qtd);
        }
    }
}
