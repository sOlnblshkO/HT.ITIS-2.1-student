using System.Diagnostics.CodeAnalysis;

namespace Hw11.Exceptions;

[ExcludeFromCodeCoverage]
public class InvalidSyntaxException : Exception
{
	public InvalidSyntaxException(string message)
		: base(message)
	{
	}
}