using Hw11.Services.Expressions;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public Task<double> CalculateMathExpressionAsync(string? expressionString)
    {
        ExpressionValidator.Validate(expressionString, out var expressions);
        var polishString = ExpressionParser.Parse(expressions);
        var expression = ExpressionTree.ConvertToExpression(polishString);
        return ExpressionTreeVisitor.VisitExpression((dynamic)expression);
    }
}