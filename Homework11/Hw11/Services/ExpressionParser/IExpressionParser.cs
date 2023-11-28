using System.Linq.Expressions;

namespace Hw11.Services.ExpressionParser;

public interface IExpressionParser
{
    public Expression Parse(string expression);
}