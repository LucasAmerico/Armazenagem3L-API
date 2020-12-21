using Armazenagem3L_API.ExceptionHandler;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Armazenagem_3LC__Tests {
    public class CargaServiceTest {

        private const int Id = 1;
        private const string Nome = "Produto";
        private const int Peso = 3200;
        private const int Preco = 3400;
        private const int Qtd = 30;
        private const int ID_PARAMETRO = 1;
        private const string Endereco = "Rua Professora Tereza Leopoldina dos Anos, 15, Silvio Pereira 1, Coronel Fabriciano, Minas gerais, 35171376";
        private const int Frete = 500;
        private const int MotoristaId = 0;
        private const int CargaId = 1;
        private const int MotoristaId1 = 1;
        private const string MOTORISTA = "Bino";
        private const int Id_MOTORISTA = 1;
        private const int Qtd_EXCEPTION = 600;
        Mock<IProdutosRepository> _produto = new Mock<IProdutosRepository>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<ICargaRepository> _repository = new Mock<ICargaRepository>();
        Mock<IMotoristaRepository> _motorista = new Mock<IMotoristaRepository>();

        [Fact]
        public void ListagemCargas_requisicaoCorreta_sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArray();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, mockProdutoLista);

            _repository.Setup(m => m.GetCargas()).Verifiable();
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(mockMotorista);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(MockProduto);



            //EXECUÇÂO
            CustomResponse actualResponse = _service.listagemCargas();

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.GetCargas(), Times.Exactly(1));

        }

        [Fact]
        public void ListagemCargas_requisicaoCorretaArrayVazio_sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArrayVazio();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, mockProdutoLista);

            _repository.Setup(m => m.GetCargas()).Verifiable();

            //EXECUÇÂO
            CustomResponse actualResponse = _service.listagemCargas();

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.GetCargas(), Times.Exactly(1));

        }

        [Fact]
        public void ListagemCargas_ErroInesperado_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);

            CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO);
            _repository.Setup(m => m.GetCargas()).Throws(new ApiCustomException(JsonSerializer.Serialize(h)));


            //EXECUÇÂO
            CustomResponse actualResponse = _service.listagemCargas();

            //ASSERTS
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            _repository.Verify(m => m.GetCargas(), Times.Exactly(1));
        }

        [Fact]
        public void CargaByIdAndMotoristaId_requisicaoCorreta_sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArray();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, mockProdutoLista);

            _repository.Setup(m => m.cargaByIdAndMotoristaId(It.IsAny<int>(), It.IsAny<int>())).Returns(MockCarga);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(mockMotorista);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(MockProduto);



            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaByIdAndMotoristaId(Id, Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.cargaByIdAndMotoristaId(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.FindCargaProdutos(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void CargaByIdAndMotoristaId_requisicaoCorretaCargaNaoEncontrada_erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArray();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, null, null);

            _repository.Setup(m => m.cargaByIdAndMotoristaId(It.IsAny<int>(), It.IsAny<int>())).Returns((Carga)null);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaByIdAndMotoristaId(Id, Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            _repository.Verify(m => m.cargaByIdAndMotoristaId(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void CargaByMotoristaId_requisicaoCorreta_sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArray();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, mockProdutoLista);

            _repository.Setup(m => m.cargaByMotoristaId(It.IsAny<int>())).Returns(mockProdutoLista.ToArray());
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(mockMotorista);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(MockProduto);



            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaByMotoristaId(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.cargaByMotoristaId(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(mockProdutoLista.Count));
            _repository.Verify(m => m.FindCargaProdutos(It.IsAny<int>()), Times.Exactly(mockProdutoLista.Count));

        }

        [Fact]
        public void CargaByMotoristaId_requisicaoCorretaArrayVazio_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockProdutoLista = MockCargaArrayVazio();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, null, mockProdutoLista);

            _repository.Setup(m => m.cargaByMotoristaId(It.IsAny<int>())).Returns(mockProdutoLista.ToArray());

            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaByMotoristaId(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            _repository.Verify(m => m.cargaByMotoristaId(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void CargaById_requisicaoCorreta_sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockCarga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, mockCarga);

            _repository.Setup(m => m.GetCargaById(It.IsAny<int>())).Returns(mockCarga);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(mockMotorista);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(MockProduto);



            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaById(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.GetCargaById(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.FindCargaProdutos(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void CargaById_requisicaoCorretaCargaNaoEncontrada_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, null, null);

            _repository.Setup(m => m.GetCargaById(It.IsAny<int>())).Returns((Carga)null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargaById(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            _repository.Verify(m => m.GetCargaById(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void Add_requisicaoCorretaProdutoNaoEncontrado_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockCarga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_BUSCA_PRODUTO), null);

            _repository.Setup(m => m.Add(It.IsAny<Carga>())).Verifiable();
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns((Produto)null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockCarga);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.Add(It.IsAny<Carga>()), Times.Exactly(1));
            _produto.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
 
        }

        [Fact]
        public void Add_requisicaoCorretaProdutoComQtdSuperior_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockCarga = MockCargaQtdException();
            var produto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_SALVAR_CARGA), null);

            _repository.Setup(m => m.Add(It.IsAny<Carga>())).Verifiable();
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(produto);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockCarga);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.Add(It.IsAny<Carga>()), Times.Exactly(1));

        }

        [Fact]
        public void Add_requisicaoCorretaErroDeBanco_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockCarga = MockCarga();
            var produto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_ADD_FALHA), null);

            _repository.Setup(m => m.Add(It.IsAny<Carga>())).Verifiable();
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(produto);
            _repository.Setup(m => m.SaveChanges()).Returns(false);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockCarga);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.Add(It.IsAny<Carga>()), Times.Exactly(1));
        }

        [Fact]
        public void Add_requisicaoCorreta_Sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockCarga = MockCarga();
            var produto = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ADD_SUCESSO), null);

            _repository.Setup(m => m.Add(It.IsAny<Carga>())).Verifiable();
            _produto.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(produto);
            _repository.Setup(m => m.SaveChanges()).Returns(true);
            _produto.Setup(m => m.Update(It.IsAny<Produto>())).Verifiable();
            _repository.Setup(m => m.AddCargaProdutos(It.IsAny<CargaProduto>())).Verifiable();
            _repository.Setup(m => m.GetLast()).Returns(MockCargaBanco);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockCarga);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.Add(It.IsAny<Carga>()), Times.Exactly(1));
        }


        [Fact]
        public void AceitarCarga_requisicaoCorretaErroBanco_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_ACEITA_ERRO), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _repository.Setup(m => m.Update(It.IsAny<Carga>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.AceitarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.Update(It.IsAny<Carga>()), Times.Exactly(1));
            _repository.Verify(m => m.SaveChanges(), Times.Exactly(1));
        }

        [Fact]
        public void AceitarCarga_requisicaoCorreta_Sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ACEITA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _repository.Setup(m => m.Update(It.IsAny<Carga>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.AceitarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.Update(It.IsAny<Carga>()), Times.Exactly(1));
            _repository.Verify(m => m.SaveChanges(), Times.Exactly(1));
        }

        [Fact]
        public void RecusarCarga_requisicaoCorretaCargaNaoEncontrada_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var motorista = mockMotorista();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns((Carga)null);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(motorista);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecusarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void RecusarCarga_requisicaoCorretaMotoristaNaoEncontrada_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var motorista = mockMotorista();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns((Motorista)null);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecusarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void RecusarCarga_requisicaoCorretaErroBanco_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var motorista = mockMotorista();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_RECUSA_ERRO), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(motorista);
            _repository.Setup(m => m.AddCargaRecusada(It.IsAny<CargasRecusada>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecusarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void RecusarCarga_requisicaoCorreta_Sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var mockMC = mockMotoristaCarga();
            var motorista = mockMotorista();
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.RECUSA_ACEITA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _motorista.Setup(m => m.FindById(It.IsAny<int>())).Returns(motorista);
            _repository.Setup(m => m.AddCargaRecusada(It.IsAny<CargasRecusada>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecusarCarga(mockMC);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _motorista.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void CargasRecusadas_requisicaoCorreta_Sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, null, null);

            _repository.Setup(m => m.FindCargasRecusadas(It.IsAny<int>())).Verifiable();


            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargasRecusadas(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.FindCargasRecusadas(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void CargasRecusadas_ErroInesperado_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);

            CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO);
            _repository.Setup(m => m.FindCargasRecusadas(It.IsAny<int>())).Throws(new ApiCustomException(JsonSerializer.Serialize(h)));


            //EXECUÇÂO
            CustomResponse actualResponse = _service.cargasRecusadas(Id);

            //ASSERTS
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            _repository.Verify(m => m.FindCargasRecusadas(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void DeletarCarga_requisicaoCorretaCargaNaoEncontrada_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns((Carga)null);


            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarCarga(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void DeletarCarga_requisicaoCorretaErroBanco_Erro() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_DELETAR_CARGA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _repository.Setup(m => m.DeleteCargaProduto(It.IsAny<CargaProduto>())).Verifiable();
            _repository.Setup(m => m.DeleteCarga(It.IsAny<Carga>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarCarga(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.FindCargaProdutos(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void DeletarCarga_requisicaoCorreta_Sucesso() {
            //PREPARACAO
            CargaService _service = new CargaService(_repository.Object, _produto.Object, _motorista.Object, _logger.Object);
            var carga = MockCarga();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_CARGA), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(carga);
            _repository.Setup(m => m.FindCargaProdutos(It.IsAny<int>())).Returns(MockCargaProdutoArray);
            _repository.Setup(m => m.DeleteCargaProduto(It.IsAny<CargaProduto>())).Verifiable();
            _repository.Setup(m => m.DeleteCarga(It.IsAny<Carga>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.DeletarCarga(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));
            _repository.Verify(m => m.FindCargaProdutos(It.IsAny<int>()), Times.Exactly(1));
        }

        //[Fact]
        //public void add_requisicaoCorreta_sucesso() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.PRODUTO_ADD_SUCESSO), null);

        //    _repository.Setup(m => m.Add(It.IsAny<Produto>())).Verifiable();
        //    _repository.Setup(m => m.SaveChanges()).Returns(true);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.Add(mockProduto);


        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        //    Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
        //    Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
        //    _repository.Verify(m => m.Add(It.IsAny<Produto>()), Times.Exactly(1));

        //}

        //[Fact]
        //public void add_requisicaoIncorreta_erro() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL), null);

        //    _repository.Setup(m => m.Add(It.IsAny<Produto>())).Verifiable();
        //    _repository.Setup(m => m.SaveChanges()).Returns(false);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.Add(mockProduto);

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
        //    Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
        //    Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
        //    _repository.Verify(m => m.Add(It.IsAny<Produto>()), Times.Exactly(1));
        //}

        //[Fact]
        //public void DeletarProduto_requisicaoCorreta_sucesso() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

        //    _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);
        //    _repository.Setup(m => m.Delete(It.IsAny<Produto>())).Verifiable();
        //    _repository.Setup(m => m.SaveChanges()).Returns(true);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        //    Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
        //    Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
        //    _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
        //    _repository.Verify(m => m.Delete(It.IsAny<Produto>()), Times.Exactly(1));

        //}

        //[Fact]
        //public void DeletarProduto_ProdutoNaoEncontrado_erro() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.PRODUTO_NAO_ENCONTRADO), null);

        //    _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns((Produto)null);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
        //    Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
        //    Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
        //    _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
        //}

        //[Fact]
        //public void DeletarProduto_ErroBancoDeletarProduto_Erro() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_DELETAR_PRODUTO), null);

        //    _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);
        //    _repository.Setup(m => m.Delete(It.IsAny<Produto>())).Verifiable();
        //    _repository.Setup(m => m.SaveChanges()).Returns(false);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.DeletarProduto(ID_PARAMETRO);

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
        //    Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
        //    Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
        //    _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));
        //    _repository.Verify(m => m.Delete(It.IsAny<Produto>()), Times.Exactly(1));

        //}

        //[Fact]
        //public void ProdutosById_requisicaoCorreta_sucesso() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

        //    _repository.Setup(m => m.GetProdutoById(It.IsAny<int>())).Returns(mockProduto);

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.produtosById(ID_PARAMETRO);

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        //    _repository.Verify(m => m.GetProdutoById(It.IsAny<int>()), Times.Exactly(1));

        //}

        //[Fact]
        //public void ListagemProdutos_requisicaoCorreta_sucesso() {
        //    //PREPARACAO
        //    ProdutosService _service = new ProdutosService(_repository.Object, _logger.Object);
        //    Produto mockProduto = MockProduto();
        //    CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_PRODUTO), null);

        //    _repository.Setup(m => m.GetProdutos()).Verifiable();

        //    //EXECUÇÂO
        //    CustomResponse actualResponse = _service.listagemProdutos();

        //    //ASSERTS
        //    Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
        //    _repository.Verify(m => m.GetProdutos(), Times.Exactly(1));

        //}

        public Produto MockProduto() {
            return new Produto(Id, Nome, Peso, Preco, Qtd_EXCEPTION);
        }

        public ArrayList MockProdutoArray() {
            ArrayList array = new ArrayList();
            array.Add(new Produto(Id, Nome, Peso, Preco, Qtd));
            array.Add(new Produto(Id, Nome, Peso, Preco, Qtd));
            array.Add(new Produto(Id, Nome, Peso, Preco, Qtd));

            return array;
        }

        public List<ProdutoQtd> MockProdutQtdArray() {
            List<ProdutoQtd> array = new List<ProdutoQtd>();
            array.Add(new ProdutoQtd(Id, Qtd, Id));
            array.Add(new ProdutoQtd(Id, Qtd, Id));
            array.Add(new ProdutoQtd(Id, Qtd, Id));

            return array;
        }

        public List<ProdutoQtd> MockProdutQtdExceptionArray() {
            List<ProdutoQtd> array = new List<ProdutoQtd>();
            array.Add(new ProdutoQtd(Id, Qtd_EXCEPTION, Id));
            array.Add(new ProdutoQtd(Id, Qtd_EXCEPTION, Id));
            array.Add(new ProdutoQtd(Id, Qtd_EXCEPTION, Id));

            return array;
        }

        public Carga MockCarga() {
            IEnumerable<ProdutoQtd> produtos = MockProdutQtdArray();
            return new Carga(Endereco, Frete, MotoristaId, produtos);
        }

        public Carga MockCargaBanco() {
            IEnumerable<ProdutoQtd> produtos = MockProdutQtdArray();
            Carga c = new Carga(Endereco, Frete, MotoristaId, produtos);
            c.Id = 1;
            return c;
        }

        public Carga MockCargaQtdException() {
            IEnumerable<ProdutoQtd> produtos = MockProdutQtdExceptionArray();
            return new Carga(Endereco, Frete, MotoristaId, produtos);
        }

        public List<Carga> MockCargaArray() {
            IEnumerable<ProdutoQtd> produtos = MockProdutQtdArray();
            List<Carga> array = new List<Carga>();
            array.Add(new Carga(Endereco, Frete, MotoristaId, produtos));
            array.Add(new Carga(Endereco, Frete, MotoristaId, produtos));
            array.Add(new Carga(Endereco, Frete, MotoristaId, produtos));

            return array;
        }

        public List<Carga> MockCargaArrayVazio() {
            List<Carga> array = new List<Carga>();
          
            return array;
        }

        public MotoristaCarga mockMotoristaCarga() {
            return new MotoristaCarga(CargaId, MotoristaId1);
        }

        public Motorista mockMotorista() {
            return new Motorista(Id_MOTORISTA, MOTORISTA);
        }

        public List<CargaProduto> MockCargaProdutoArray() {
            List<CargaProduto> array = new List<CargaProduto>();
            array.Add(new CargaProduto(Id, Id, Qtd));
            array.Add(new CargaProduto(Id, Id, Qtd));
            array.Add(new CargaProduto(Id, Id, Qtd));

            return array;
        }

    }
}
