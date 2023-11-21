using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.ExpressionCalculator;

public interface IExpressionCalculator
{
    public Task<double> CalculateAsync(Expression expression);
}