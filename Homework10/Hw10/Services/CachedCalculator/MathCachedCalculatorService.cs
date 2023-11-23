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

    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        var cache = 
            await _dbContext.SolvingExpressions.FirstOrDefaultAsync(exp => exp.Expression.Equals(expression));

        if (cache is not null)
            return new CalculationMathExpressionResultDto(cache.Result);

        var result = await _simpleCalculator.CalculateMathExpressionAsync(expression);

        if (result.IsSuccess)
        {
            await _dbContext.AddAsync(new SolvingExpression()
                {Result = result.Result, Expression = expression!});
            await _dbContext.SaveChangesAsync();
        }

        return result;
    }
}