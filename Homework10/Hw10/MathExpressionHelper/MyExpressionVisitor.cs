using System.Linq.Expressions;

namespace Hw10.MathExpressionHelper;

/// <summary>
/// Класс отвечающий за проход по всем вершинам Expression
/// </summary>
public class MyExpressionVisitor : ExpressionVisitor
{
    private readonly ExpressionCalculator _expressionCalculator;

    public MyExpressionVisitor(ExpressionCalculator calculator)
    {
        _expressionCalculator = calculator;
    }

    /// <summary>
    /// Метод посещающий вершину и добавляющий её в список выражений калькулятора
    /// </summary>
    public override Expression? Visit(Expression? node)
    {
        if(node is not null && !_expressionCalculator.ExecuteBefore.ContainsKey(node))
            _expressionCalculator.AddExpression(node);
        
        return base.Visit(node);
    }
}