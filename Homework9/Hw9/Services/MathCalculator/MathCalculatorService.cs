using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.Services.Expressions;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expressionString)
    {
        try
        {
            ExpressionValidator.Validate(expressionString, out var expressions);
            var polishString = ExpressionParser.Parse(expressions);
            var expression = ExpressionTree.ConvertToExpression(polishString);
            var result = Expression.Lambda<Func<double>>(
                await ExpressionTreeVisitor.VisitExpression(expression)).Compile().Invoke();
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}