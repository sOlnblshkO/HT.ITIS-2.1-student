using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;

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
		var cachedResult = _dbContext.SolvingExpressions.FirstOrDefault(exp => exp.Expression.Equals(expression));
		if (cachedResult != null)
		{
			await Task.Delay(1000);
			return new CalculationMathExpressionResultDto(cachedResult.Result);
		}

		var result = await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (result.IsSuccess)
		{
			_dbContext.SolvingExpressions.Add(new SolvingExpression { Expression = expression!, Result = result.Result });
			await _dbContext.SaveChangesAsync();
		}
		return result;
	}
}