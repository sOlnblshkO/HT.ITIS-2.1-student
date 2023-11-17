using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator;

public class ExpressionBuilder
{
    public static Expression ConvertToExpression(string postfixForm)
    {
        var result = new Stack<Expression>();
        foreach (var part in postfixForm.Split())
        {
            if (double.TryParse(part, out var val))
            {
                result.Push(Expression.Constant(val));
                continue;
            }
            var right = result.Pop();
            var left = result.Pop();
            var node = part switch
            {
                "+" => Expression.Add(left, right),
                "-" => Expression.Subtract(left, right),
                "*" => Expression.Multiply(left, right),
                "/" => Expression.Divide(left, right),
                 _ => throw new Exception()
            };

            result.Push(node);
        }

        return result.Pop();
    }
}