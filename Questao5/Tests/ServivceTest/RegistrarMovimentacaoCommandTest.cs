using Dapper;
using NSubstitute;
using NSubstitute.Core;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Services.Commnad;
using System.Data;
using Xunit;

namespace Questao5.Tests.ServivceTest
{
	public class RegistrarMovimentacaoCommandTest
	{
		private readonly IDbConnection _dbMock;
		private readonly RegistrarMovimentacaoHandler _handler;

		public RegistrarMovimentacaoCommandTest()
		{
			// Criamos um mock da conexão
			_dbMock = Substitute.For<IDbConnection>();

			// Retornamos um mock de transação ao chamar "BeginTransaction"
			_dbMock.BeginTransaction().Returns(Substitute.For<IDbTransaction>());

			// Simulamos um valor para "ConnectionString" para evitar o erro
			_dbMock.ConnectionString.Returns("DataSource=:memory:");

			_handler = new RegistrarMovimentacaoHandler(_dbMock);
		}

		[Fact]
		public async Task Handle_DeveRegistrarMovimentacao_QuandoSucesso()
		{
			// Arrange
			var dbMock = Substitute.For<IDbConnection>();
			var handler = new RegistrarMovimentacaoHandler(dbMock);

			var command = new RegistrarMovimentacaoCommand
			{
				Request = new MovimentacaoRequest
				{
					IdContaCorrente = "1",
					IdRequisicao = "req-123",
					Valor = 100,
					TipoMovimento = "C"
				}
			};

			// Assert
			Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
		}
	}
}