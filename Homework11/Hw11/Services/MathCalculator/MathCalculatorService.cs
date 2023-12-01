using System.Linq.Expressions;
using Hw11.Dto;
using Hw11.Exceptions;
using Hw11.Services.Expressions;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ExpressionValidator.Validate(expression, out var expressions);
        var polishString = ExpressionParser.Parse(expressions);
        var exp = ExpressionTree.ConvertToExpression(polishString);
        var result = Expression.Lambda<Func<double>>(
            await ExpressionTreeVisitor.VisitExpression(exp)).Compile().Invoke();
        return new CalculationMathExpressionResultDto(result).Result;
    }
}