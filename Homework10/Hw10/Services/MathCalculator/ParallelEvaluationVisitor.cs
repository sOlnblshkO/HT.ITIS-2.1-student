using System.Linq.Expressions;
using Hw10.ErrorMessages;

namespace Hw10.Services.MathCalculator;

public class ParallelEvaluationVisitor : ExpressionVisitor
{
    public static Task<Expression> VisitExpression(Expression expression)
    {
        return Task.Run(() => new ParallelEvaluationVisitor().Visit(expression));
    }
    
    protected override Expression VisitBinary(BinaryExpression node)
    {
        try
        {
            return VisitBinaryAsync(node).Result;
        }
        catch (AggregateException e)
        {
            throw e.InnerException;
        }
    }

    private async Task<Expression> VisitBinaryAsync(BinaryExpression node)
    {
        var firstTask = new Lazy<Task<Expression>>(async () =>
        {
            await Task.Delay(1000);
            if (node.Left is BinaryExpression binaryLeft)
                return await VisitBinaryAsync(binaryLeft);
            return node.Left;
        });
        var secondTask = new Lazy<Task<Expression>>(async () =>
        {
            await Task.Delay(1000);
            if (node.Right is BinaryExpression binaryLeft)
                return await VisitBinaryAsync(binaryLeft);
            return node.Right;
        });

        var result = await Task.WhenAll(firstTask.Value, secondTask.Value);

        if (node.NodeType == ExpressionType.Divide)
        {
            if (Expression.Lambda<Func<double>>(result[1]).Compile().Invoke() == 0)
                throw new Exception(MathErrorMessager.DivisionByZero);
        }

        return node.NodeType switch
        {
            ExpressionType.Add => Expression.Add(result[0], result[1]),
            ExpressionType.Subtract => Expression.Subtract(result[0], result[1]),
            ExpressionType.Multiply => Expression.Multiply(result[0], result[1]),
            ExpressionType.Divide => Expression.Divide(result[0], result[1]),
            _ => throw new InvalidOperationException("Operation not supported")
        };
    }
}