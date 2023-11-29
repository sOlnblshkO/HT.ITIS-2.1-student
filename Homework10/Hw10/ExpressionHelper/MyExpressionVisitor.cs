using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw10.ErrorMessages;

namespace Hw10.ExpressionHelper;

[ExcludeFromCodeCoverage]
public class MyExpressionVisitor : ExpressionVisitor
{
    public static Task<Expression> VisitExpression(Expression expression) =>
        Task.Run(() => new MyExpressionVisitor().Visit(expression));

    protected override Expression VisitUnary(UnaryExpression node)
    {
        var value = CompileUnaryAsync(node).Result;
        return GetExpressionByMathOperationType(node.NodeType, new[] { value });
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        var values = CompileBinaryAsync(node.Left, node.Right).Result;
        return GetExpressionByMathOperationType(node.NodeType, values);
    }

    private static async Task<double> CompileUnaryAsync(Expression expression)
    {
        await Task.Delay(1000);

        var expressionCompiled = Task.Run(() => Expression.Lambda<Func<double>>(expression).Compile().Invoke());

        return await expressionCompiled;
    }
    
    private static async Task<double[]> CompileBinaryAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);

        var first = Task.Run(() => Expression.Lambda<Func<double>>(left).Compile().Invoke());
        var second = Task.Run(() => Expression.Lambda<Func<double>>(right).Compile().Invoke());

        return await Task.WhenAll(first, second);
    }

    private static Expression GetExpressionByMathOperationType(ExpressionType expressionType, double[] expressionValues)
    {
        return expressionType switch
        {
            ExpressionType.Negate => 
                Expression.Negate(Expression.Constant(expressionValues[0])),
            ExpressionType.Add =>
                Expression.Add(Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            ExpressionType.Subtract =>
                Expression.Subtract(Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            ExpressionType.Multiply =>
                Expression.Multiply(Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            _ => expressionValues[1] == 0
                ? throw new ArgumentException(MathErrorMessager.DivisionByZero)
                : Expression.Divide(Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1]))
        };
    }
}