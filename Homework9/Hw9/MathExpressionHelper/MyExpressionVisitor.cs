using System.Linq.Expressions;
using Hw9.ErrorMessages;

namespace Hw9.MathExpressionHelper;

public class MyExpressionVisitor : ExpressionVisitor
{
      public static Task<Expression> VisitExpression(Expression expression) =>
        Task.Factory.StartNew(() => new MyExpressionVisitor().Visit(expression));

    protected override Expression VisitUnary(UnaryExpression node)
    {
        Task.WaitAny(Task.Run(() => Visit(node.Operand)));
        
        var value = CompileUnaryAsync(node).Result;

        return ExpressionByType(node.NodeType, value);
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {

        var firstTask = Task.Run(() => Visit(node.Left));
        var secondTask = Task.Run(() => Visit(node.Right));

        Task.WaitAll(firstTask, secondTask);
        
        var (firstValue, secondValue) = CompileBinaryAsync(node).Result;

        return ExpressionByType(node.NodeType, firstValue, secondValue);
    }

    private static async Task<(double, double)> CompileBinaryAsync(BinaryExpression expression)
    {
        await Task.Delay(1000);

        var firstTask = Task.Factory.
            StartNew(() =>Expression.Lambda<Func<double>>(expression.Left).Compile().Invoke());
        var secondTask = Task.Factory.
            StartNew(() => Expression.Lambda<Func<double>>(expression.Right).Compile().Invoke());

        var values = await Task.WhenAll(firstTask, secondTask);
        
        return (values[0], values[1]);
    }

    private static async Task<double> CompileUnaryAsync(UnaryExpression expression)
    {
        await Task.Delay(1000);

        var compiledExpressionTask = Task.Factory.
            StartNew(() => Expression.Lambda<Func<double>>(expression).Compile().Invoke());

        return await compiledExpressionTask;
    }

    private static Expression ExpressionByType(ExpressionType type, double firstValue, double secondValue = 0)
    {
        var childLeft = Expression.Constant(firstValue);
        var childRight = Expression.Constant(secondValue);

        return type switch
        {
            ExpressionType.Negate => Expression.Negate(childLeft),
            ExpressionType.Add => Expression.Add(childLeft, childRight),
            ExpressionType.Subtract => Expression.Subtract(childLeft, childRight),
            ExpressionType.Multiply => Expression.Multiply(childLeft, childRight),
            ExpressionType.Divide => secondValue != 0
                ? Expression.Divide(childLeft, childRight)
                : throw new Exception(MathErrorMessager.DivisionByZero),
            _ => throw new InvalidOperationException($"ExpressionType {type} doesn't supporting")
        };
    }
}