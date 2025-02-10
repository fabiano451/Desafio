using System;
using System.Globalization;

namespace Questao1 {
	class Program
	{
		static void Main()
		{
			Console.Write("Entre o número da conta: ");
			int numero = int.Parse(Console.ReadLine());

			Console.Write("Entre o titular da conta: ");
			string titular = Console.ReadLine();

			Console.Write("Haverá depósito inicial (s/n)? ");
			char resposta = char.Parse(Console.ReadLine().ToLower());

			ContaBancaria conta;

			if (resposta == 's')
			{
				Console.Write("Entre o valor de depósito inicial: ");
				double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
				conta = new ContaBancaria(numero, titular, depositoInicial);
			}
			else
			{
				conta = new ContaBancaria(numero, titular);
			}

			Console.WriteLine("\nDados da conta:");
			Console.WriteLine(conta);

			Console.Write("\nEntre um valor para depósito: ");
			double deposito = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
			conta.Depositar(deposito);
			Console.WriteLine("Dados da conta atualizados:");
			Console.WriteLine(conta);

			Console.Write("\nEntre um valor para saque: ");
			double saque = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
			conta.Sacar(saque);
			Console.WriteLine("Dados da conta atualizados:");
			Console.WriteLine(conta);
		}
	}
}
