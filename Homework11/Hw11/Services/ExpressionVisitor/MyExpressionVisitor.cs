using System.Linq.Expressions;
using Hw11.ErrorMessages;
using System.Diagnostics.CodeAnalysis;

namespace Hw11.Services.MyExpressionVisitor;

[ExcludeFromCodeCoverage]
public class MyExpressionVisitor : IExpressionVisitor
{
    public Expression VisitExpression(Expression expression) =>
        new MyExpressionVisitor().Visit((dynamic)expression);
    
    private Expression Visit(ConstantExpression cnst) =>
        cnst;

    private Expression Visit(BinaryExpression expr)
    {
        var expressionValues =  CompileAsync(expr.Left, expr.Right).Result;
        return GetExpressionByType(expr.NodeType, expressionValues);
    }
    
    private async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);
        var t1 = Task.Run(() => Expression.Lambda<Func<double>>(VisitExpression(left)).Compile().Invoke());
        var t2 = Task.Run(() => Expression.Lambda<Func<double>>(VisitExpression(right)).Compile().Invoke());
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