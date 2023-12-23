using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.MathExpressionHelper;

namespace Hw9.Services.MathCalculator;

/// <summary>
/// Сервис, отвечающий за вычисление арифметических выражений
/// </summary>
public class MathCalculatorService : IMathCalculatorService
{
    /// <summary>
    /// Возвращает результа арифметического выражения
    /// </summary>
    /// <param name="expression">Арифметическое выражение</param>
    /// <returns>Результат выражения</returns>
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        var validationResultMessage = await ExpressionValidator.CheckForCorrectExpressionAsync(expression);

        if (validationResultMessage is not ExpressionValidator.Correct)
            return new CalculationMathExpressionResultDto(validationResultMessage);
        
        var expressionInPolishNotation = new ExpressionParser().ToPolishNotation(expression!);

        var expressionTree = ExpressionTreeConverter.ToExpressionTree(expressionInPolishNotation);

        try
        {
            var result = await new ExpressionCalculator().CalculateExpressionAsync(expressionTree);

            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}