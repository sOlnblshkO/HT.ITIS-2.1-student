using System.Linq.Expressions;
using Hw11.Services.ExpressionUtils;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ExpressionValidator.Validate(expression);
        var polishNotation = ExpressionToPolishNotationParser.ToReversePolishNotation(expression!);

        var exp = ExpressionBuilder.ConvertToExpression(polishNotation);
        
        return await ExpressionTreeVisitorImpl.VisitExpressionAsync(exp);
    }
}