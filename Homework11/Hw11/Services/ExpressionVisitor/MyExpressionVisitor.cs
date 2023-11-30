using System.Linq.Expressions;
using Hw11.ErrorMessages;
using System.Diagnostics.CodeAnalysis;

namespace Hw11.Services.MyExpressionVisitor;

[ExcludeFromCodeCoverage]
public class MyExpressionVisitor : IExpressionVisitor
{
    public async Task<Expression> VisitExpression(Expression expression)
    {
        return await new MyExpressionVisitor().Visit((dynamic)expression);
    }

    private Task<Expression> Visit(ConstantExpression cnst) =>
        Task.FromResult((Expression)cnst);

    private async Task<Expression> Visit(BinaryExpression expr)
    {
        var expressionValues = await CompileAsync(expr.Left, expr.Right);
        return GetExpressionByType(expr.NodeType, expressionValues);
    }
    
    private async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);
        var t1 = Task.Run(async () => Expression.Lambda<Func<double>>(await VisitExpression(left)).Compile().Invoke());
        var t2 = Task.Run(async () => Expression.Lambda<Func<double>>(await VisitExpression(right)).Compile().Invoke());
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