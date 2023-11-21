using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Hw10.Dto;
using Hw10.ExpressionHelper;

namespace Hw10.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            var validatorMessage = ExpressionValidator.Validate(expression!);
            if (!validatorMessage.Equals("OK"))
                return new CalculationMathExpressionResultDto(validatorMessage);

            var expressionParser = new ExpressionParser();

            var splitDelimiter = new Regex("(?<=[-+*/()])|(?=[-+*/()])");
            var expressionInPolishNotation = expressionParser.ParseToPolishNotation(splitDelimiter.Split(expression!));

            var expressionConverted = ExpressionTreeConverter.ConvertToExpressionTree(expressionInPolishNotation);

            await Task.Delay(1000);
            
            var result = Expression.Lambda<Func<double>>(
                await MyExpressionVisitor.VisitExpression(expressionConverted)).Compile().Invoke();
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception e)
        {
            return new CalculationMathExpressionResultDto(e.Message);
        }

    }
}