using Hw10.DbModels;
using Hw10.Dto;
using Microsoft.EntityFrameworkCore;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
    private readonly ApplicationContext _dbContext;
    private readonly IMathCalculatorService _simpleCalculator;

    public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
    {
        _dbContext = dbContext;
        _simpleCalculator = simpleCalculator;
    }

    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expressionString)
    {
        if (expressionString == null)
            return await _simpleCalculator.CalculateMathExpressionAsync(expressionString);

        var expression = await _dbContext.SolvingExpressions
            .FirstOrDefaultAsync(x => x.Expression == expressionString);

        if (expression != null)
            return new CalculationMathExpressionResultDto(expression.Result);

        var response = await _simpleCalculator.CalculateMathExpressionAsync(expressionString);

        if (!response.IsSuccess)
            return response;

        var currentExpression = new SolvingExpression()
        {
            Expression = expressionString,
            Result = response.Result
        };

        _dbContext.SolvingExpressions.Add(currentExpression);

        return response;
    }
}