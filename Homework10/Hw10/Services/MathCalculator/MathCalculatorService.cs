using Hw10.Dto;
using Hw10.ErrorMessages;
using Hw10.Services.MathCalculator.ExpressionCalculator;
using Hw10.Services.MathCalculator.ExpressionParser;

namespace Hw10.Services.MathCalculator;

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