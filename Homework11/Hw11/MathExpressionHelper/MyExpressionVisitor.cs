using System.Linq.Expressions;

namespace Hw11.MathExpressionHelper;

/// <summary>
/// Класс отвечающий за проход по всем вершинам Expression
/// </summary>
public class MyExpressionVisitor
{
    private readonly ExpressionCalculator _expressionCalculator;

    public MyExpressionVisitor(ExpressionCalculator calculator)
    {
        _expressionCalculator = calculator;
    }

    /// <summary>
    /// Метод посещающий вершину и добавляющий её в список выражений калькулятора
    /// </summary>
    public void Visit(Expression? node)
    {
        if (node is null)
            return;
        
        if(!_expressionCalculator.ExecuteBefore.ContainsKey(node))
            _expressionCalculator.AddExpression(node);
        
        if(node.NodeType is not ExpressionType.Constant)
            VisitExpressionNode(node as dynamic);
    }

    protected void VisitExpressionNode(UnaryExpression node)
    {
        Visit(node.Operand);
    }

    protected void VisitExpressionNode(BinaryExpression node)
    {
        Visit(node.Left);
        Visit(node.Right);
    }
    
    
}