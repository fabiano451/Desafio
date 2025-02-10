using Dapper;
using MediatR;
using Questao5;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Services.Commnad;
using System.Data;

public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, SaldoResponse>
{
	private readonly IDbConnection _db;

	public ConsultarSaldoHandler(IDbConnection db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db));
	}

	public async Task<SaldoResponse> Handle(ConsultarSaldoQuery query, CancellationToken cancellationToken)
	{
		var conta = await _db.QueryFirstOrDefaultAsync<dynamic>(
			"SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
			new { query.IdContaCorrente });

		if (conta == null) throw new ContaInvalidaException();
		if (conta.ativo == 0) throw new ContaInativaException();

		var creditos = await _db.ExecuteScalarAsync<decimal>(
			"SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'C'",
			new { Id = query.IdContaCorrente });

		var debitos = await _db.ExecuteScalarAsync<decimal>(
			"SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'D'",
			new { Id = query.IdContaCorrente });

		return new SaldoResponse
		{
			NumeroConta = conta.numero,
			NomeTitular = conta.nome,
			DataHoraConsulta = DateTime.UtcNow,
			Saldo = creditos - debitos
		};
	}
}
