using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Hw9.Dto;
using Hw9.ExpressinHelper;


namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        Thread.Sleep(2000);
        
        try
        {
            var validatorMessage = ExpressionValidator.Validate(expression!);
            if (!validatorMessage.Equals("OK"))
                return new CalculationMathExpressionResultDto(validatorMessage);

            var expressionParser = new ExpressionParser();

            var splitDelimiter = new Regex("(?<=[-+*/()])|(?=[-+*/()])");
            var expressionInPolishNotation = expressionParser.ParseToPolishNotation(splitDelimiter.Split(expression!));

            var expressionConverted = ExpressionTreeConverter.ConvertToExpressionTree(expressionInPolishNotation);

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