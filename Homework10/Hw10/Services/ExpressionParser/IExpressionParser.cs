using System.Linq.Expressions;

namespace Hw10.Services.ExpressionParser;

public interface IExpressionParser
{
    public Expression Parse(string expression);
}