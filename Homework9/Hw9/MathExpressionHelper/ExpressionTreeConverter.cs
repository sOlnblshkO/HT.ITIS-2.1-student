using System.Globalization;
using System.Linq.Expressions;
using Hw9.Extensions;

namespace Hw9.MathExpressionHelper;

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
    
    public static Expression ToExpressionTree(string expression)
    {
        var expressionTokens = expression.Split(" ").Without("");
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