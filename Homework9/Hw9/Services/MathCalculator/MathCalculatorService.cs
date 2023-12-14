using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.MathExpressionHelper;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        var validationResultMessage = await ExpressionValidator.CheckForCorrectExpressionAsync(expression);
        
        if (validationResultMessage is not ExpressionValidator.Correct)
            return new CalculationMathExpressionResultDto(validationResultMessage);

        var expressionParser = new ExpressionParser();
        var expressionInPolishNotation = expressionParser.ToPolishNotation(expression!);
        
        var expressionTree = ExpressionTreeConverter.ToExpressionTree(expressionInPolishNotation);

        try
        {
            var result = Expression.Lambda<Func<double>>(new MyExpressionVisitor().Visit(expressionTree))
                .Compile()
                .Invoke();

            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception ex) { return new CalculationMathExpressionResultDto(ex.Message); }
    }
}