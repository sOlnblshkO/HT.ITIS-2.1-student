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
        var solvingExpression =
            await _dbContext.SolvingExpressions.FirstOrDefaultAsync(se => se.Expression == expression);
        if (solvingExpression != null)
        {
            await Task.Delay(1000);
            return new CalculationMathExpressionResultDto(solvingExpression.Result);
        }

        var res = await _simpleCalculator.CalculateMathExpressionAsync(expression);
        if (res.IsSuccess)
        {
            await _dbContext.SolvingExpressions.AddAsync(new SolvingExpression
                { Result = res.Result, Expression = expression! });
            await _dbContext.SaveChangesAsync();
        }

        return res;
    }
}