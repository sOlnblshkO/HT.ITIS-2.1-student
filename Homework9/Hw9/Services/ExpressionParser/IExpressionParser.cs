using System.Linq.Expressions;

namespace Hw9.Services.ExpressionParser;

public interface IExpressionParser
{
    public Expression Parse(string expression);
}