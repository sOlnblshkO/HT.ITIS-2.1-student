using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Hw9.Services;

public static class MathParserService
{
    public static Expression ParseExpression(string expression)
    {
        var expressionTransformed = ConvertToPolakNotation(expression);
        var operations = new Stack<Expression>();

        foreach (var s in expressionTransformed)
        {
            if (Regex.IsMatch(s, @"^-?\d+(\.\d+)?$"))
            {
                operations.Push(Expression.Constant(double.Parse(s)));
                continue;
            }

            var right = operations.Pop();
            var left = operations.Pop();

            switch (s)
            {
                case "+":
                    operations.Push(Expression.Add(left, right));
                    break;
                case "-":
                    operations.Push(Expression.Subtract(left, right));
                    break;
                case "*":
                    operations.Push(Expression.Multiply(left, right));
                    break;
                case "/":
                    operations.Push(Expression.Divide(left, right));
                    break;
                default:
                    throw new InvalidOperationException("Operation not supported");
            }
        }

        return operations.Pop();
    }

    private static string[] ConvertToPolakNotation(string original)
    {
        var operators = new Dictionary<char, int>
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 }
        };

        var output = "";
        var stack = new Stack<char>();

        for (var i = 0; i < original.Length; i++)
        {
            if (char.IsDigit(original[i]) || original[i] == '.' )
            {
                output += original[i];
            }
            else if (original[i].Equals('-') && char.IsDigit(original[i+1]))
            {
                output += original[i];
            }
            else if (operators.TryGetValue(original[i], out var @operator))
            {
                output += " ";

                while (stack.Count > 0 && operators.ContainsKey(stack.Peek()) &&
                       @operator <= operators[stack.Peek()])
                {
                    output += stack.Pop();
                    output += " ";
                }

                stack.Push(original[i]);
            }
            else if (original[i] == '(')
            {
                stack.Push(original[i]);
            }
            else if (original[i] == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    output += " ";
                    output += stack.Pop();
                }

                stack.Pop();
            }
        }

        while (stack.Count > 0)
        {
            output += " ";
            output += stack.Pop();
        }

        return output.Trim().Split();
    }
}