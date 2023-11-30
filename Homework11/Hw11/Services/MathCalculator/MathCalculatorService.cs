using System.Linq.Expressions;
using Hw11.Services.ExpressionParser;
using Hw11.Services.ExpressionValidator;
using Hw11.Services.MyExpressionVisitor;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    private readonly IExpressionValidator _expressionValidator;
    private readonly IExpressionParser _expressionParser;
    private readonly IExpressionVisitor _expressionVisitor;
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        _expressionValidator.Validate(expression);
        var parsedExpression = _expressionParser.Parse(expression!);
        var func =
            Expression.Lambda<Func<double>>(await _expressionVisitor.VisitExpression(parsedExpression));
        return func.Compile().Invoke();
    }
    public MathCalculatorService(IExpressionValidator expressionValidator, IExpressionParser expressionParser, IExpressionVisitor expressionVisitor)
    {
        _expressionValidator = expressionValidator;
        _expressionParser = expressionParser;
        _expressionVisitor = expressionVisitor;
    }
}