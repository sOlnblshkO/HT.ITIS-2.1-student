using System.Linq.Expressions;

namespace Hw9.Services.ExpressionCalculator;

public interface IExpressionCalculator
{
    public Task<double> CalculateAsync(Expression expression);
}