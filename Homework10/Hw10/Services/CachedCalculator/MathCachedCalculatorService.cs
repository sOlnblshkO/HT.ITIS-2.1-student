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
		if (!string.IsNullOrEmpty(expression))
		{
			var solvingExpressions = _dbContext.SolvingExpressions;
			var solvingExpressionsWhere = solvingExpressions.Where(i => i.Expression == expression.Trim());
			if (solvingExpressionsWhere.Any())
			{
				var expressionResult = solvingExpressionsWhere.First().Result;
				
				await Task.Delay(500);
				
				return new CalculationMathExpressionResultDto(expressionResult);
			}
		}
		
		
		var result = await _simpleCalculator.CalculateMathExpressionAsync(expression);

		if (!result.IsSuccess) return new CalculationMathExpressionResultDto(result.ErrorMessage);
		var solvingExpression = new SolvingExpression
		{
			SolvingExpressionId = 0,
			Expression = expression!.Trim(),
			Result = result.Result
		};
			
		_dbContext.SolvingExpressions.Add(solvingExpression);
		await _dbContext.SaveChangesAsync();
			
		return new CalculationMathExpressionResultDto(result.Result);

	}
}