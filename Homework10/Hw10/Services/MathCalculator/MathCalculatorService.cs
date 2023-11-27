using System.Linq.Expressions;
using Hw10.Dto;
using Hw10.Services.ExpressionUtils;

namespace Hw10.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            var message = ExpressionValidator.Validate(expression);
            if (message != null) 
            {
                return new CalculationMathExpressionResultDto(message);
            }
            var polishNotation = ExpressionToPolishNotationParser.ToReversePolishNotation(expression!);
            var tree = ExpressionBuilder.ConvertToExpression(polishNotation);

            var res = await ExpressionTreeVisitorImpl.VisitBinary(tree);
            return new CalculationMathExpressionResultDto(res){IsSuccess = true};
            
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}