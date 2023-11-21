using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.ExpressionCalculator;

public class MathExpressionCalculator : ExpressionVisitor, IExpressionCalculator
{
    private List<Expression> _topSortTempResult = new();
    private Dictionary<Expression, List<Expression>> ExecuteBefore { get; } = new();
    private ConcurrentDictionary<Expression, double> Result { get; } = new();

    public async Task<double> CalculateAsync(Expression expression)
    {
        _topSortTempResult.Clear();
        ExecuteBefore.Clear();
        Result.Clear();
        Visit(expression);
        var lazy = new Dictionary<Expression, Lazy<Task>>();
        foreach (var (nextExpression, dependencies) in ExecuteBefore)
            lazy[nextExpression] = new Lazy<Task>(async () =>
            {
                await Task.WhenAll(dependencies.Select(dependency => lazy[dependency].Value));
                await Task.Yield();
                if (nextExpression is BinaryExpression binaryExpression)
                    await CalculateBinaryAsync(binaryExpression);
                if (nextExpression is UnaryExpression unaryExpression)
                    await CalculateUnaryAsync(unaryExpression);
            });
        await Task.WhenAll(lazy.Values.Select(l => l.Value));
        if (expression is ConstantExpression constantExpression)
            return (double)constantExpression.Value!;
        return Result[expression];
    }

    private double GetResult(Expression expression)
    {
        if (expression is ConstantExpression constantExpression)
            return (double)constantExpression.Value!;
        return Result[expression];
    }

    [ExcludeFromCodeCoverage]
    private async Task<double> CalculateBinaryAsync(BinaryExpression expression)
    {
        await Task.Delay(1000);
        var left = GetResult(expression.Left);
        var right = GetResult(expression.Right);
        var result = expression.NodeType switch
        {
            ExpressionType.Add => left + right,
            ExpressionType.Subtract => left - right,
            ExpressionType.Divide => left / right,
            ExpressionType.Multiply => left * right,
            _ => throw new InvalidOperationException()
        };
        Result[expression] = result;
        return Result[expression];
    }

    [ExcludeFromCodeCoverage]
    private async Task<double> CalculateUnaryAsync(UnaryExpression expression)
    {
        await Task.Delay(1000);
        var operand = GetResult(expression.Operand);
        var result = expression.NodeType switch
        {
            ExpressionType.Negate => -operand,
            _ => throw new InvalidOperationException()
        };
        Result[expression] = result;
        return Result[expression];
    }


    protected override Expression VisitBinary(BinaryExpression node)
    {
        var previousTempResult = _topSortTempResult;
        _topSortTempResult = new List<Expression>();
        var result = base.VisitBinary(node);
        ExecuteBefore.Add(result, _topSortTempResult);
        _topSortTempResult = previousTempResult;
        _topSortTempResult.Add(result);
        return result;
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
        var previousTempResult = _topSortTempResult;
        _topSortTempResult = new List<Expression>();
        var result = base.VisitUnary(node);
        ExecuteBefore[result] = _topSortTempResult;
        _topSortTempResult = previousTempResult;
        _topSortTempResult.Add(result);
        return result;
    }
}