using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;
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
			.FirstOrDefaultAsync(x => x.Expression.Equals(expressionString));

		if (expression != null)
			return new CalculationMathExpressionResultDto(expression.Result);

		var resultDto = await _simpleCalculator
			.CalculateMathExpressionAsync(expressionString);
		
		if (!resultDto.IsSuccess)
			return resultDto;
		
		await _dbContext.SolvingExpressions.AddAsync(
			new SolvingExpression
			{
				Expression = expressionString, 
				Result = resultDto.Result
			});
		await _dbContext.SaveChangesAsync();

		return resultDto;
	}
}