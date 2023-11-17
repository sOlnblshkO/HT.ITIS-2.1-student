using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Services.ExpressionCalculator;
using Hw9.Services.ExpressionParser;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    private readonly IExpressionCalculator _expressionCalculator;
    private readonly IExpressionParser _expressionParser;

    public MathCalculatorService(IExpressionParser expressionParser, IExpressionCalculator expressionCalculator)
    {
        _expressionParser = expressionParser;
        _expressionCalculator = expressionCalculator;
    }

    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            var parsed = _expressionParser.Parse(expression!);
            var result = await _expressionCalculator.CalculateAsync(parsed);
            return double.IsFinite(result)
                ? new CalculationMathExpressionResultDto(result)
                : new CalculationMathExpressionResultDto(MathErrorMessager.DivisionByZero);
        }
        catch (ArgumentException e)
        {
            return new CalculationMathExpressionResultDto(e.Message);
        }
    }
}