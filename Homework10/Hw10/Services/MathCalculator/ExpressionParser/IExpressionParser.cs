using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.ExpressionParser;

public interface IExpressionParser
{
    public Expression Parse(string expression);
}