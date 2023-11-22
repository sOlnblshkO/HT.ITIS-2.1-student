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
		try
		{
			var solvingExpression =
				await _dbContext.SolvingExpressions.FirstOrDefaultAsync(se => se.Expression == expression);
			if (solvingExpression != null)
			{
				return new CalculationMathExpressionResultDto(solvingExpression.Result);
			}

			var calculationResult = await _simpleCalculator.CalculateMathExpressionAsync(expression);

			if (calculationResult.IsSuccess)
			{
				var newSolvingExpression = new SolvingExpression
				{
					Result = calculationResult.Result,
					Expression = expression!
				};
				await _dbContext.SolvingExpressions.AddAsync(newSolvingExpression);
				await _dbContext.SaveChangesAsync();
			}

			return calculationResult;
		}
		catch (Exception e)
		{
			return new CalculationMathExpressionResultDto(e.Message);
		}
	}
}