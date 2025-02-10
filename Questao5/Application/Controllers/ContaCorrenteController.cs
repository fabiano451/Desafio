using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Questao5;
using Questao5.Infrastructure.Services.Commnad;

namespace Questao5.Application.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ContaCorrenteController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ContaCorrenteController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Movimenta uma conta corrente (Crédito ou Débito)
		/// </summary>
		[HttpPost("movimentacao")]
		public async Task<IActionResult> RegistrarMovimentacao([FromBody] RegistrarMovimentacaoCommand command)
		{
			try
			{
				var resultado = await _mediator.Send(command);
				return Ok(new { idMovimento = resultado });
			}
			catch (ContaInvalidaException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (ContaInativaException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (ValorInvalidoException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (TipoMovimentoInvalidoException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { erro = ex.Message });
			}
		}

		/// <summary>
		/// Consulta o saldo de uma conta corrente
		/// </summary>
		[HttpGet("saldo/{idConta}")]
		public async Task<IActionResult> ConsultarSaldo(string idConta)
		{
			try
			{
				var query = new ConsultarSaldoQuery { IdContaCorrente = idConta };
				var resultado = await _mediator.Send(query);
				return Ok(resultado);
			}
			catch (ContaInvalidaException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (ContaInativaException ex)
			{
				return BadRequest(new { erro = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { erro = ex.Message });
			}
		}
	}
}


