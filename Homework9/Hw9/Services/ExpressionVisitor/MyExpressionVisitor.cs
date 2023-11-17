using System.Linq.Expressions;
using Hw9.ErrorMessages;
using System.Diagnostics.CodeAnalysis;

namespace Hw9.Services.MyExpressionVisitor;

[ExcludeFromCodeCoverage]
public class MyExpressionVisitor : ExpressionVisitor, IExpressionVisitor
{
    public Task<Expression> VisitExpression(Expression expression) =>
        Task.Run(() => new MyExpressionVisitor().Visit(expression));

    protected override Expression VisitBinary(BinaryExpression node)
    {
        var expressionValues = CompileAsync(node.Left, node.Right).Result;
        return GetExpressionByType(node.NodeType, expressionValues);
    }

    private async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);
        var t1 = Task.Run(() => Expression.Lambda<Func<double>>(VisitExpression(left).Result).Compile().Invoke());
        var t2 = Task.Run(() => Expression.Lambda<Func<double>>(VisitExpression(right).Result).Compile().Invoke());
        return await Task.WhenAll(t1, t2);
    }
    private Expression GetExpressionByType(ExpressionType expressionType, IReadOnlyList<double> values)
    {
        Func<Expression, Expression, Expression> expr = expressionType switch
        {
            ExpressionType.Add => Expression.Add,
            ExpressionType.Subtract => Expression.Subtract,
            ExpressionType.Multiply => Expression.Multiply,
            ExpressionType.Divide => Expression.Divide
        };
        if (expressionType is ExpressionType.Divide && values[1] <= double.Epsilon)
            throw new DivideByZeroException(MathErrorMessager.DivisionByZero);

        return expr(Expression.Constant(values[0]), Expression.Constant(values[1]));
    }
}