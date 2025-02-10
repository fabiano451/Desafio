using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Infrastructure.Services.Commnad
{
	public class RegistrarMovimentacaoCommand : IRequest<string>
	{
		public MovimentacaoRequest Request { get; set; }
	}

}
