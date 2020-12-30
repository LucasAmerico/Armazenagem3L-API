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
    public class MotoristaServiceTest {
        private const int Id = 1;
        private const string Nome = "Bino";
        private const string Login = "bino";
        private const string Senha = "cargapesada";
        private const string Email = "bino_cilada@gmail.com";
        Mock<IMotoristaRepository> _repository = new Mock<IMotoristaRepository>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();

        [Fact]
        public void add_requisicaoCorreta_sucesso() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.MOTORISTA_ADD_SUCESSO), null);

            _repository.Setup(m => m.Add(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns((Motorista) null);
            _repository.Setup(m => m.FindByLogin(It.IsAny<string>())).Returns((Motorista) null);
            _repository.Setup(m => m.SaveChanges()).Returns(true);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockMotorista);


            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Add(It.IsAny<Motorista>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByLogin(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void add_requisicaoIncorretaEmailExistente_erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_EMAIL), null);

            _repository.Setup(m => m.Add(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns(mockMotorista);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockMotorista);


            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void add_requisicaoIncorretaLoginExistente_erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_LOGIN), null);

            _repository.Setup(m => m.Add(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns((Motorista)null);
            _repository.Setup(m => m.FindByLogin(It.IsAny<string>())).Returns(mockMotorista);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockMotorista);


            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByLogin(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void add_requisicaoCorretaErroBanco_erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL), null);

            _repository.Setup(m => m.Add(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns((Motorista)null);
            _repository.Setup(m => m.FindByLogin(It.IsAny<string>())).Returns((Motorista)null);
            _repository.Setup(m => m.SaveChanges()).Returns(false);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.Add(mockMotorista);


            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Add(It.IsAny<Motorista>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByLogin(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void RecuperarSenha_requisicaoCorreta_sucesso() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            DadosMotorista mockDados = MockDadosMotorista();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.SENHA_ATT_SUCESSO), null);

           
            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns(mockMotorista);
            _repository.Setup(m => m.Update(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(true);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecuperarSenha(mockDados);


            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Update(It.IsAny<Motorista>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void RecuperarSenha_requisicaoCorretaEmailNaoEcontrado_Erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            DadosMotorista mockDados = MockDadosMotorista();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO), null);

            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns((Motorista) null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecuperarSenha(mockDados);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void RecuperarSenha_requisicaoCorretaErroBanco_Erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            DadosMotorista mockDados = MockDadosMotorista();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.ERRO_GERAL), null);


            _repository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns(mockMotorista);
            _repository.Setup(m => m.Update(It.IsAny<Motorista>())).Verifiable();
            _repository.Setup(m => m.SaveChanges()).Returns(false);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.RecuperarSenha(mockDados);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.Update(It.IsAny<Motorista>()), Times.Exactly(1));
            _repository.Verify(m => m.FindByEmail(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void Login_requisicaoCorreta_sucesso() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            DadosMotorista mockDados = MockDadosMotorista();

            _repository.Setup(m => m.FindByLogin(It.IsAny<string>())).Returns(mockMotorista);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.login(mockDados);

            //ASSERTS
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.FindByLogin(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void Login_requisicaoCorretaLoginNaoEcontrado_Erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            DadosMotorista mockDados = MockDadosMotorista();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO), null);

            _repository.Setup(m => m.FindByLogin(It.IsAny<string>())).Returns((Motorista)null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.login(mockDados);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.FindByLogin(It.IsAny<string>()), Times.Exactly(1));

        }

        [Fact]
        public void FindById_requisicaoCorreta_sucesso() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.SENHA_ATT_SUCESSO), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns(mockMotorista);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.FindById(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));

        }

        [Fact]
        public void FindById_requisicaoIncoreetaMotoristaNaoEncontrado_Erro() {
            //PREPARACAO
            MotoristaService _service = new MotoristaService(_repository.Object, _logger.Object);
            Motorista mockMotorista = MockProduto();
            CustomResponse expectResponse = new CustomResponse(HttpStatusCode.UnprocessableEntity, new CustomMessage(Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO), null);

            _repository.Setup(m => m.FindById(It.IsAny<int>())).Returns((Motorista) null);

            //EXECUÇÂO
            CustomResponse actualResponse = _service.FindById(Id);

            //ASSERTS
            Assert.Equal(actualResponse.StatusCode, expectResponse.StatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, actualResponse.StatusCode);
            Assert.Equal(actualResponse.Mensagem.Descricao, expectResponse.Mensagem.Descricao);
            Assert.Equal(actualResponse.Mensagem.Nome, expectResponse.Mensagem.Nome);
            _repository.Verify(m => m.FindById(It.IsAny<int>()), Times.Exactly(1));

        }

        public Motorista MockProduto() {
            return new Motorista(Id, Nome, Login, Senha, Email);
        }

        public DadosMotorista MockDadosMotorista() {
            return new DadosMotorista(Senha, Email, Login);
        }
    }
}
