namespace Questao5.Tests.ServivceTest
{
	using System;
	using System.Data;
	using System.Threading;
	using System.Threading.Tasks;
	using Dapper;
	using NSubstitute;
	using Xunit;
	using Questao5.Infrastructure.Services.Commnad;
	using Questao5.Infrastructure.Database.QueryStore.Responses;
	using SQLitePCL;



	public class ConsultarSaldoHandlerTests
	{
		private readonly IDbConnection _mockDb;
		private readonly ConsultarSaldoHandler _handler;

		public ConsultarSaldoHandlerTests()
		{
			_mockDb = Substitute.For<IDbConnection>();
			_handler = new ConsultarSaldoHandler(_mockDb);
		}

		

		[Fact]
		public void Handle_DeveLancarExcecao_QuandoContaInativa()
		{
			// Arrange
			var query = new ConsultarSaldoQuery { IdContaCorrente = "123" };
			var contaInativa = new { numero = "123456", nome = "João", ativo = 0 };

			// Act & Assert
			 Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(query, CancellationToken.None));
		}
		
	}
}