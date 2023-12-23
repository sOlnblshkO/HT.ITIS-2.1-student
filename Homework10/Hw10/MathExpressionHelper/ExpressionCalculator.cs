using System.Linq.Expressions;
using Hw10.ErrorMessages;

namespace Hw10.MathExpressionHelper;

/// <summary>
/// Калькулятор Expression выражений
/// </summary>
public class ExpressionCalculator
{
    public readonly Dictionary<Expression, Expression[]> ExecuteBefore = new ();

    /// <summary>
    /// Добавление арифметического выражения в словарь с выражением, и выражениями, котрые надо быполнить до этого
    /// </summary>
    /// <param name="expression"></param>
    public void AddExpression(Expression expression)
    {
        if (expression is BinaryExpression binary)
            ExecuteBefore[binary] = new[] { binary.Left, binary.Right };
        
        else if (expression is UnaryExpression unary)
            ExecuteBefore[unary] = new[] { unary.Operand };
        
        else if (expression is ConstantExpression constant)
            ExecuteBefore[constant] = Array.Empty<Expression>();
    }
    
    /// <summary>
    /// Вычисляет Expression Tree
    /// </summary>
    /// <param name="expression">Expression Tree</param>
    /// <returns>Результат Expression Tree</returns>
    public async Task<double> CalculateExpressionAsync(Expression expression)
    {
        new MyExpressionVisitor(this).Visit(expression);
        
        var lazy = new Dictionary<Expression, Lazy<Task<double>>>();

        foreach (var (after, before) in ExecuteBefore)
        {
            lazy[after] = new (async () =>
            {
                if (!before.Any())
                    return CalculateExpressionNode(after);
                
                await Task.WhenAll(before.Select(expr => lazy[expr].Value));
                await Task.Yield();

                await Task.Delay(1000);

                return after switch
                {
                    BinaryExpression binary => CalculateExpressionNode(binary, await lazy[binary.Left].Value, await lazy[binary.Right].Value),
                    UnaryExpression unary => CalculateExpressionNode(unary, await lazy[unary.Operand].Value),
                    ConstantExpression => CalculateExpressionNode(after),
                    _ => throw new Exception($"Unavailable expression type {after.Type.Name}")
                };
            });
        }
        
        return await lazy[expression].Value;
    }

    /// <summary>
    /// Вычисляет узел Expression Tree
    /// </summary>
    /// <param name="expression">Арифметическое выражение</param>
    /// <param name="operands"></param>
    /// <returns>Результат выражения</returns>
    private static double CalculateExpressionNode(Expression expression, params double[] operands)
    {
        return expression.NodeType switch
        {
            ExpressionType.Add => operands[0] + operands[1],
            ExpressionType.Subtract => operands[0] - operands[1],
            ExpressionType.Multiply => operands[0] * operands[1],
            ExpressionType.Divide =>
                Math.Abs(operands[1]) > double.Epsilon
                    ? operands[0] / operands[1] 
                    : throw new DivideByZeroException(MathErrorMessager.DivisionByZero),
            ExpressionType.Negate => -operands[0],
            ExpressionType.Constant => (double)(expression as ConstantExpression)?.Value!,
            _ => throw new Exception($"Unavailable expression node type {expression.NodeType}")
        };
    }
}