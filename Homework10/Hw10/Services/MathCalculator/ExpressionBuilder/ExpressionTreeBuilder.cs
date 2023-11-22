using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw10.ErrorMessages;

namespace Hw10.Services.MathCalculator.ExpressionBuilder;

[ExcludeFromCodeCoverage]
public class ExpressionTreeBuilder
{
    public static Expression CreateExpressionTree(string input)
    {
        var stack = new Stack<Expression>();
        foreach (var elem in input.Split(" "))
        {
            if (elem == "" || elem == " ")
                continue;
            if (double.TryParse(elem, out var val))
                stack.Push(Expression.Constant(val));
            else
            {
                var right = stack.Pop();
                var left = stack.Pop();
                
                stack.Push(elem switch
                {
                    "+" => Expression.Add(left,right),
                    "-" => Expression.Subtract(left,right),
                    "*" => Expression.Multiply(left,right),
                    "/" => Expression.Divide(left,right),
                    _ => throw new Exception(MathErrorMessager.UnknownCharacter)
                });
                
            }
        }
        return stack.Pop();
    }
}