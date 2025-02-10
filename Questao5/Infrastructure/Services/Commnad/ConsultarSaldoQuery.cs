using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Services.Commnad
{
	public class ConsultarSaldoQuery: IRequest<SaldoResponse>
	{
		public string IdContaCorrente { get; set; }
	}
}
