using System.Text.RegularExpressions;
using Hw11.ExpressionHelper;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ExpressionValidator.Validate(expression);
        var expressionParser = new ExpressionParser();

        var splitDelimiter = new Regex("(?<=[-+*/()])|(?=[-+*/()])");
        var expressionInPolishNotation = expressionParser.ParseToPolishNotation(splitDelimiter.Split(expression!));

        var expressionConverted = ExpressionTreeConverter.ConvertToExpressionTree(expressionInPolishNotation);

        await Task.Delay(1000);
        
        var result = await MyExpressionVisitor.VisitExpression(expressionConverted);
        return result;
    }
}