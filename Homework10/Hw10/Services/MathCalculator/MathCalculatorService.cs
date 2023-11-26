using Hw10.Dto;
using Hw10.Services.MathCalculator.ExpressionBuilder;
using Hw10.Services.MathCalculator.ParserAndValidator;

namespace Hw10.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            Validator.Validate(expression!);
            var parse = Parser.ConvertToPostfixForm(expression!);
            var tree = ExpressionTreeBuilder.CreateExpressionTree(parse);
            var result = await ExpressionTreeVisitor.VisitAsync(tree);
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception e)
        {
            return new CalculationMathExpressionResultDto(e.Message);
        }
    }
}