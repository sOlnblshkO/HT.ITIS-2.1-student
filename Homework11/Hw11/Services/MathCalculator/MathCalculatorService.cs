using Hw11.Dto;
using Hw11.MathExpressionHelper;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ExpressionValidator.CheckForCorrectExpression(expression);
        
        var expressionInPolishNotation = new ExpressionParser().ToPolishNotation(expression!);

        var expressionTree = ExpressionTreeConverter.ToExpressionTree(expressionInPolishNotation);
        
        var result = await new ExpressionCalculator().CalculateExpressionAsync(expressionTree);
        
        return result;
    }
}