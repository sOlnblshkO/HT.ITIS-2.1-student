using System.Text;
using Hw9.ErrorMessages;

namespace Hw9.Services.ExpressionTokenizer;

public class MathExpressionTokenizer : IExpressionTokenizer
{
    public List<string> Tokenize(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException(MathErrorMessager.EmptyString);
        var result = new List<string>();
        var currentToken = new StringBuilder();
        var maybeUnary = true;
        foreach (var character in expression.Where(character => !char.IsWhiteSpace(character)))
        {
            if (maybeUnary)
            {
                maybeUnary = false;
                if (character == '-')
                {
                    result.Add("0-");
                    continue;
                }
            }

            if (character is ')' or '(' or '+' or '-' or '*' or '/')
            {
                if (character == '(')
                    maybeUnary = true;
                if (currentToken.Length > 0)
                    result.Add(currentToken.ToString());
                currentToken.Clear();
                result.Add(character.ToString());
                continue;
            }

            if (char.IsDigit(character) || character == '.')
            {
                currentToken.Append(character);
                continue;
            }

            throw new ArgumentException(MathErrorMessager.UnknownCharacterMessage(character));
        }

        if (currentToken.Length > 0)
            result.Add(currentToken.ToString());
        return result;
    }
}