using System.Diagnostics.CodeAnalysis;

namespace Hw11.Exceptions;

[ExcludeFromCodeCoverage]
public class InvalidSymbolException: Exception
{
	public InvalidSymbolException(string message)
		: base(message)
	{
	}
}