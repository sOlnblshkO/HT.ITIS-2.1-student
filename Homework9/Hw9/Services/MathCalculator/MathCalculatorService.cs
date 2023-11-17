using System.Linq.Expressions;
using Hw9.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        // ?? ладно не буду переделывать когда делал валидатор не думал что там будут исключения
        // которые надо будет лоавить не в валидаторе
        try
        {
            var message = ExpressionValidator.Validate(expression);
            if (message != null) 
            {
                return new CalculationMathExpressionResultDto(message);
            }
            var polishNotation = ExpressionToPolishNotationParser.ToReversePolishNotation(expression!);
            var tree = ExpressionBuilder.ConvertToExpression(polishNotation);

            var exp = await ExpressionTreeVisitorImpl.VisitExpression(tree);
            var result = ((Func<double>)Expression.Lambda(exp).Compile()).Invoke();
            return new CalculationMathExpressionResultDto(result);
            
        }
        catch (Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}