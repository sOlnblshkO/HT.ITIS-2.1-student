using System.Text.RegularExpressions;

namespace Hw10.Services.MathCalculator;

using System.Collections.Generic;

public class ExpressionToPolishNotationParser {
    private static Regex _numbers = new(@"^\d+");
    private static Regex _delimiters = new("(?<=[-+*/()])|(?=[-+*/()])");

    private static readonly Dictionary<string, int> Priorities = new()
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };
    
    public static string ToReversePolishNotation(string expression)
    {
        string[] expressions = _delimiters.Split(expression.Replace(" ",""));
        var operations = new Stack<string>();
        var polish = new Stack<string>();
        var considerMinusAsNegation = true;
        for (var i = 0; i < expressions.Length; i++)
        {
            var item = expressions[i];
            if (item.Length == 0) continue;
            if (_numbers.IsMatch(item))
            {
                polish.Push(item);
                considerMinusAsNegation = false;
                continue;
            }
            switch (item)
            {
                case "-" when considerMinusAsNegation:
                    polish.Push(item + expressions[++i]);
                    considerMinusAsNegation = false;
                    continue;
                case "(":
                    operations.Push(item);
                    considerMinusAsNegation = true;
                    continue;
                case ")":
                    while (operations.Peek() != "(")
                        PushExpression(operations, polish);
                    operations.Pop();
                    considerMinusAsNegation = false;
                    continue;
            }

            while (operations.Count > 0 && Priorities[item] <= Priorities[operations.Peek()])
                PushExpression(operations, polish);

            operations.Push(item);
            considerMinusAsNegation = true;
        }

        while (operations.Count > 0)
            PushExpression(operations, polish);

        return polish.Pop();
    }

    private static void PushExpression(Stack<string> operations, Stack<string> polish)
    {
        var op = operations.Pop();
        var arg1 = polish.Pop();
        var arg2 = polish.Pop();
        polish.Push(arg2 + " " + arg1 + " " + op);
    }
}
