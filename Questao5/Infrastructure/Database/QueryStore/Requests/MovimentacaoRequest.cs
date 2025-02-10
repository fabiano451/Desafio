namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
	public class MovimentacaoRequest
	{
		public string IdRequisicao { get; set; }
		public string IdContaCorrente { get; set; }
		public decimal Valor { get; set; }
		public string TipoMovimento { get; set; } // "C" ou "D"
	}
}
