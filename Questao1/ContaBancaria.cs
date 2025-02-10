using System;
using System.Globalization;

namespace Questao1
{
	class ContaBancaria
	{
		public int Numero { get; }
		public string Titular { get; private set; }
		private double Saldo;
		private const double TAXA_SAQUE = 3.50;

		public ContaBancaria(int numero, string titular, double depositoInicial = 0.0)
		{
			Numero = numero;
			Titular = titular;
			Saldo = depositoInicial;
		}

		public void Depositar(double valor)
		{
			Saldo += valor;
		}

		public void Sacar(double valor)
		{
			Saldo -= (valor + TAXA_SAQUE);
		}

		public void AlterarTitular(string novoTitular)
		{
			Titular = novoTitular;
		}

		public override string ToString()
		{
			return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
		}
	}

}


