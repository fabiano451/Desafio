namespace Questao5.Tests.Controllers
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using FluentAssertions;
	using MediatR;
	using Microsoft.AspNetCore.Mvc;
	using NSubstitute;
	using NSubstitute.ExceptionExtensions;
	using Questao5.Application.Controllers;
	using Questao5.Application;
	using Questao5.Infrastructure.Database.QueryStore.Responses;
	using Questao5.Infrastructure.Services.Commnad;
	using Xunit;

	public class ContaCorrenteControllerTests
	{
		private readonly IMediator _mediator;
		private readonly ContaCorrenteController _controller;


		public ContaCorrenteControllerTests()
		{
			_mediator = Substitute.For<IMediator>();
			_controller = new ContaCorrenteController(_mediator);
		}

		[Fact]
		public async Task ConsultarSaldo_DeveRetornar200_QuandoContaValida()
		{
			// Arrange
			var idConta = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
			var responseMock = new SaldoResponse
			{
				NumeroConta = 123,
				NomeTitular = "Katherine Sanchez",
				DataHoraConsulta = DateTime.UtcNow,
				Saldo = 500.00m
			};

			_mediator.Send(Arg.Any<ConsultarSaldoQuery>()).Returns(Task.FromResult(responseMock));

			// Act
			var result = await _controller.ConsultarSaldo(idConta);

			// Assert
			var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var saldoResponse = okResult.Value.Should().BeAssignableTo<SaldoResponse>().Subject;
			saldoResponse.Saldo.Should().Be(500.00m);
		}

		[Fact]
		public async Task ConsultarSaldo_DeveRetornar400_QuandoContaInvalida()
		{
			// Arrange
			var idConta = "INVALID_ID";
			_mediator.Send(Arg.Any<ConsultarSaldoQuery>()).ThrowsAsync(new Exception("INVALID_ACCOUNT"));

			// Act
			var result = await _controller.ConsultarSaldo(idConta);

			// Assert
			var badRequestResult = result.Should().NotBeNull();
			
		}

		[Fact]
		public async Task RegistrarMovimentacao_DeveRetornarOk_QuandoSucesso()
		{
			// Arrange
			var command = new RegistrarMovimentacaoCommand { /* Dados do comando */ };
			var movimentoId = "12340BGT";
			_mediator.Send(command).Returns(movimentoId);

			// Act
			var result = await _controller.RegistrarMovimentacao(command);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var response = okResult.Value as dynamic;
			Assert.NotNull(response);
			Assert.Equal(movimentoId, response.idMovimento);
		}

		[InlineData(typeof(ContaInativaException))]
		[InlineData(typeof(ValorInvalidoException))]
		[InlineData(typeof(TipoMovimentoInvalidoException))]
		public async Task RegistrarMovimentacao_DeveRetornarBadRequest_QuandoExcecaoEsperada(Type exceptionType)
		{
			// Arrange
			var command = new RegistrarMovimentacaoCommand();
			var exception = (Exception)Activator.CreateInstance(exceptionType, "Erro de teste");
			_mediator.Send(command).Throws( new Exception());

			// Act
			var result = await _controller.RegistrarMovimentacao(command);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var response = Assert.IsType<dynamic>(badRequestResult.Value);
			Assert.Equal("Erro de teste", response.erro);
		}
	}

}


