using Hw9.Dto;
using Hw9.Services.ExpressionTree;
using Hw9.Services.ParserAndValidator;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            Validator.Validate(expression);
            var parse = Parser.ConvertToPostfixForm(expression);
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