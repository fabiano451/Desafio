using Dapper;
using MediatR;
using Newtonsoft.Json;
using Questao5.Infrastructure.Services.Commnad;
using System.Data;

public class RegistrarMovimentacaoHandler : IRequestHandler<RegistrarMovimentacaoCommand, string>
{
	private readonly IDbConnection _db;

	public RegistrarMovimentacaoHandler(IDbConnection db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db));
	}

	public async Task<string> Handle(RegistrarMovimentacaoCommand command, CancellationToken cancellationToken)
	{
		var req = command.Request;

		// Validações básicas
		var conta = await _db.QueryFirstOrDefaultAsync<dynamic>(
			"SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
			new { req.IdContaCorrente });

		if (conta == null) throw new ContaInvalidaException();
		if (conta.ativo == 0) throw new ContaInativaException();
		if (req.Valor <= 0) throw new ValorInvalidoException();
		if (req.TipoMovimento != "C" && req.TipoMovimento != "D") throw new TipoMovimentoInvalidoException();

		// Implementação de idempotência
		var existeRequisicao = await _db.QueryFirstOrDefaultAsync<string>(
			"SELECT resultado FROM idempotencia WHERE chave_idempotencia = @IdRequisicao",
			new { req.IdRequisicao });

		if (existeRequisicao != null) return existeRequisicao;

		string idMovimento = Guid.NewGuid().ToString();
		await _db.ExecuteAsync(
			"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @Conta, @Data, @Tipo, @Valor)",
			new { Id = idMovimento, Conta = req.IdContaCorrente, Data = DateTime.UtcNow.ToString("dd/MM/yyyy"), Tipo = req.TipoMovimento, Valor = req.Valor });

		// Salvar idempotência
		await _db.ExecuteAsync(
	"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Id, @Req, @Res)",
	new { Id = req.IdRequisicao, Req = JsonConvert.SerializeObject(req), Res = idMovimento });


		return idMovimento;
	}
}
