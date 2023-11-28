using System.Diagnostics.CodeAnalysis;

namespace Hw11.Exceptions;

[ExcludeFromCodeCoverage]
public class InvalidNumberException: Exception
{
	public InvalidNumberException(string message)
		: base(message)
	{
	}
}