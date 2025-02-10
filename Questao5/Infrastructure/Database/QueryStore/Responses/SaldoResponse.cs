namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
	public class SaldoResponse
	{
		public long NumeroConta { get; set; }
		public string NomeTitular { get; set; }
		public DateTime DataHoraConsulta { get; set; }
		public decimal? Saldo { get; set; }
	}
}
