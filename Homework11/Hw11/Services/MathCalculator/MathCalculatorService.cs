using Hw11.Services.MathCalculator.ExpressionBuilder;
using Hw11.Services.MathCalculator.ParseAndValidate;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        Validator.Validate(expression!);
        var parse = Parser.ConvertToPostfixForm(expression!);
        dynamic tree = ExpressionTreeBuilder.CreateExpressionTree(parse);
        return await ExpressionTreeVisitor.VisitExpression(tree);
    }
}