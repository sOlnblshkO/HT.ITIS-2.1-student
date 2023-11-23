using System.Linq.Expressions;
using Hw10.Dto;
using Hw10.Services.ExpressionParser;
using Hw10.Services.ExpressionValidator;
using Hw10.Services.MyExpressionVisitor;

namespace Hw10.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    private readonly IExpressionValidator _expressionValidator;
    private readonly IExpressionParser _expressionParser;
    private readonly IExpressionVisitor _expressionVisitor;
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            _expressionValidator.Validate(expression);
            var parsedExpression = _expressionParser.Parse(expression!);
            var func = 
                Expression.Lambda<Func<double>>(_expressionVisitor.VisitExpression(parsedExpression));
            return new CalculationMathExpressionResultDto(func.Compile().Invoke());
        }
        catch(Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
    public MathCalculatorService(IExpressionValidator expressionValidator, IExpressionParser expressionParser, IExpressionVisitor expressionVisitor)
    {
        _expressionValidator = expressionValidator;
        _expressionParser = expressionParser;
        _expressionVisitor = expressionVisitor;
    }
}