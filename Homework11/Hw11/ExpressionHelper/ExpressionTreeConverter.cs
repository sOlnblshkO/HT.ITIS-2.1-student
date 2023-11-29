using System.Linq.Expressions;

namespace Hw11.ExpressionHelper;

public class ExpressionTreeConverter
{
    public static Expression ConvertToExpressionTree(string expressionString)
    {
        var expressions = expressionString.Split(" ")
            .Where(i => i != "")
            .ToArray();
        var expressionStack = new Stack<Expression>();

        foreach (var expressionElement in expressions)
        {
            if (double.TryParse(expressionElement, out var operand))
            {
                expressionStack.Push(Expression.Constant(operand));
            }
            else
            {
                if (expressionElement == "~")
                {
                    var value = expressionStack.Pop();
                    expressionStack.Push(Expression.Negate(value));
                }
                else
                {
                    var right = expressionStack.Pop();
                    var left = expressionStack.Pop();

                    var expression = expressionElement switch
                    {
                        "+" => Expression.Add(left, right),
                        "-" => Expression.Subtract(left, right),
                        "*" => Expression.Multiply(left, right),
                        _ => Expression.Divide(left, right)
                    };
                
                    expressionStack.Push(expression);
                }
            }
        }

        return expressionStack.Pop();
    }
}