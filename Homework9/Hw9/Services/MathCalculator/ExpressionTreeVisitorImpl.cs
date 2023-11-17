using System.Linq.Expressions;
using static Hw9.ErrorMessages.MathErrorMessager;

namespace Hw9.Services.MathCalculator;

public class ExpressionTreeVisitorImpl: ExpressionVisitor
{
    protected override Expression VisitBinary(BinaryExpression head)
    {
        Console.WriteLine(head.Left.NodeType);
        var result = CompileLeftAndRight(head.Left, head.Right).Result;
        return head.NodeType switch
        {
            ExpressionType.Add => Expression.Add(Expression.Constant(result[0]), Expression.Constant(result[1])),
            ExpressionType.Subtract => Expression.Subtract(Expression.Constant(result[0]),
                Expression.Constant(result[1])),
            ExpressionType.Multiply => Expression.Multiply(Expression.Constant(result[0]),
                Expression.Constant(result[1])),
            ExpressionType.Divide when result[1] != 0 => Expression.Divide(Expression.Constant(result[0]),
                Expression.Constant(result[1])),
            ExpressionType.Divide when result[1] == 0 => throw new Exception(DivisionByZero),
            _ => throw new Exception()
        };
    }

    public static async Task<Expression> VisitExpression(Expression expression) =>
        await Task.Run(() => new ExpressionTreeVisitorImpl().Visit(expression));

    private static async Task<double[]> CompileLeftAndRight(Expression left, Expression right)
    {
        await Task.Delay(3000);
        var leftPart = Task.Run(() => ((Func<double>)Expression.Lambda(left).Compile()).Invoke());
        var rightPart = Task.Run(() => ((Func<double>)Expression.Lambda(right).Compile()).Invoke());
        return await Task.WhenAll(leftPart, rightPart);
    }
}