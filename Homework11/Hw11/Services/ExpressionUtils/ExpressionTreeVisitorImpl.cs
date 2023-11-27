using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw11.ErrorMessages;
using Microsoft.AspNetCore.Mvc;

namespace Hw11.Services.ExpressionUtils;

public class ExpressionTreeVisitorImpl
{
    public static async Task<double> VisitExpressionAsync(Expression expression)
    {
        return await VisitAsync((dynamic)expression);
    }

    private static async Task<double> VisitAsync(BinaryExpression expression)
    {
        await Task.Delay(1000);
        var left = Task.Run(() => VisitExpressionAsync(expression.Left));
        var right = Task.Run(() => VisitExpressionAsync(expression.Right));
        var result = await Task.WhenAll(left, right);
        
        return GetResult(expression, result[0], result[1]);
    }

    private static async Task<double> VisitAsync(ConstantExpression expression)
    {
        return (double)expression.Value!;
    }

    public static double GetResult(Expression binaryExpr, double value1, double value2)
    {
        return binaryExpr.NodeType switch
        {
            ExpressionType.Add => value1 + value2,
            ExpressionType.Subtract => value1 - value2,
            ExpressionType.Multiply => value1 * value2,
            ExpressionType.Divide => (value2 < double.Epsilon)
                ? throw new DivideByZeroException(MathErrorMessager.DivisionByZero)
                : value1 / value2,
            _ => throw new Exception(MathErrorMessager.UnknownCharacter)
        };
    }

}