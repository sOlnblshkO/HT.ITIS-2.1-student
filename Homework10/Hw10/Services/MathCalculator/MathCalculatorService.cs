using System.Linq.Expressions;
using Hw10.Dto;
using Hw10.Services.Expressions;

namespace Hw10.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            ExpressionValidator.Validate(expression, out var expressions);
            var polishString = ExpressionParser.Parse(expressions);
            var exp = ExpressionTree.ConvertToExpression(polishString);
            var result = Expression.Lambda<Func<double>>(
                await ExpressionTreeVisitor.VisitExpression(exp)).Compile().Invoke();
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}