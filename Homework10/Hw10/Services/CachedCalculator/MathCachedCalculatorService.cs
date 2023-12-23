using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Extensions;

namespace Hw10.Services.CachedCalculator;
/// <summary>
/// Калькулятор, который кэширует арифметические выражения и их результаты в базу данных
/// а затем, если понадобится результат этого выражения, калькулятор просто достанет его из базы данных
/// </summary>
public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}
	
	/// <summary>
	/// Вычисляет значение арифметического выражения, если оно не закэшировано, и кэширует его в базу данных,
	/// иначе же берёт выражение и его результат из базы данных
	/// </summary>
	/// <param name="expression">Арифметическое выражение</param>
	/// <returns>Результат выражения CalculationMathExpressionResultDto</returns>
	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		expression = expression?.WithoutSpaces();

		var cachedExpression =
			_dbContext.SolvingExpressions.FirstOrDefault(expr =>
				expr.Expression.Equals(expression));

		if (cachedExpression is not null)
		{
			await Task.Delay(1000);
			return new CalculationMathExpressionResultDto(cachedExpression.Result);
		}

		var resultDto = await _simpleCalculator.CalculateMathExpressionAsync(expression);

		if (!resultDto.IsSuccess)
			return new CalculationMathExpressionResultDto(resultDto.ErrorMessage);

		var expressionResultCache = new SolvingExpression() { Expression = expression!, Result = resultDto.Result };

		await _dbContext.AddAsync(expressionResultCache);
		await _dbContext.SaveChangesAsync();

		return resultDto;
	}
}