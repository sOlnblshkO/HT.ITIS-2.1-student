using System.Linq.Expressions;

namespace Hw11.Services.Expressions;

public static class ExpressionTree
{
    public static Expression ConvertToExpression(string expression)
    {
        var stack = new Stack<Expression>();
        foreach (var token in expression.Split())
        {
            if (double.TryParse(token, out var val))
            {
                stack.Push(Expression.Constant(val));
                continue;
            }
            var right = stack.Pop();
            var left = stack.Pop();
            var node = token switch
            {
                "+" => Expression.Add(left, right),
                "-" => Expression.Subtract(left, right),
                "*" => Expression.Multiply(left, right),
                _ => Expression.Divide(left, right)
            };

            stack.Push(node);
        }

        return stack.Pop();
    }
}