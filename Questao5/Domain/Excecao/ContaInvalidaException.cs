public class ContaInvalidaException : Exception
{
	public ContaInvalidaException() : base("INVALID_ACCOUNT") { }
}

public class ContaInativaException : Exception
{
	public ContaInativaException() : base("INACTIVE_ACCOUNT") { }
}

public class ValorInvalidoException : Exception
{
	public ValorInvalidoException() : base("INVALID_VALUE") { }
}

public class TipoMovimentoInvalidoException : Exception
{
	public TipoMovimentoInvalidoException() : base("INVALID_TYPE") { }
}
