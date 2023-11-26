using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using static Hw11.ErrorMessages.MathErrorMessager;

namespace Hw11.Services.Expressions;

[ExcludeFromCodeCoverage]
public static class ExpressionTreeVisitor
{
    private static async Task<double> VisitNode(BinaryExpression root)
    {
        await Task.Delay(1000);
        var left = VisitExpression(root.Left);
        var right = VisitExpression(root.Right);
        Task.WaitAll(left, right);

        return root.NodeType switch
        {
            ExpressionType.Add => left.Result + right.Result,
            ExpressionType.Subtract => left.Result - right.Result,
            ExpressionType.Multiply => left.Result * right.Result,
            _ => right.Result < double.Epsilon
                ? throw new DivideByZeroException(DivisionByZero)
                : left.Result / right.Result
        };
    }

    private static async Task<double> VisitNode(ConstantExpression node)
    {
        return await Task.FromResult((double)node.Value!);
    }

    public static async Task<double> VisitExpression(Expression expr)
    {
        return await VisitNode((dynamic)expr);
    }
}