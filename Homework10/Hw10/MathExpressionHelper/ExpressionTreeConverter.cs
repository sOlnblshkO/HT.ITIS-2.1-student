using System.Globalization;
using System.Linq.Expressions;

namespace Hw10.MathExpressionHelper;

/// <summary>
/// Класс, отвечающий за построение дерева выражений (Epxression Tree)
/// </summary>
public class ExpressionTreeConverter
{
    private static readonly Dictionary<string, Func<Expression, Expression, Expression>> ExpressionByOperation;

    static ExpressionTreeConverter()
    {
        ExpressionByOperation = new()
        {
            { "+", Expression.Add },
            { "-", Expression.Subtract },
            { "*", Expression.Multiply },
            { "/", Expression.Divide }
        };
    }
    
    /// <summary>
    /// Переводит арифметическое выражение в обратной польской записи в дерево выражений (Expression Tree)
    /// </summary>
    /// <param name="expression">Арифметическое выражение в обратной польской записи</param>
    /// <returns></returns>
    public static Expression ToExpressionTree(string expression)
    {
        var expressionTokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var expressionStack = new Stack<Expression>();
        
        foreach (var token in expressionTokens)
        {
            if(double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var operand))
                expressionStack.Push(Expression.Constant(operand));
            
            else if (token == "~" && expressionStack.TryPop(out var number))
                expressionStack.Push(Expression.Negate(number));
            
            else if (ExpressionValidator.IsOperation(token)
                     && expressionStack.TryPop(out var rightChild)
                     && expressionStack.TryPop(out var leftChild))
                expressionStack.Push(ExpressionByOperation[token](leftChild, rightChild));
        }

        return expressionStack.Pop();
    }
}