using static Hw10.Patterns.Patterns;

namespace Hw10.Services.Expressions;

public static class ExpressionParser
{
    private static readonly Dictionary<string, int> Priorities = new()
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };

    public static string Parse(string[] expressions) => ReverseToPolishNotation(expressions);
    
    private static string ReverseToPolishNotation(string[] expressions)
    {
        var ops = new Stack<string>();
        var polish = new Stack<string>();
        var isOpenParenthesis = true;
        for (var i = 0; i < expressions.Length; i++)
        {
            var token = expressions[i];
            if (token.Length == 0) continue;
            if (NumPattern.IsMatch(token))
            {
                polish.Push(token);
                isOpenParenthesis = false;
                continue;
            }
            if (token == "-" && isOpenParenthesis)
            {
                polish.Push(token + expressions[++i]);
                isOpenParenthesis = false;
                continue;
            }
            switch (token)
            {
                case "(":
                    ops.Push(token);
                    isOpenParenthesis = true;
                    continue;
                case ")":
                {
                    while (ops.Peek() != "(")
                        PushExpression(ops, polish);
                    ops.Pop();
                    isOpenParenthesis = false;
                    continue;
                }
            }

            while (ops.Count > 0 && Priorities[token] <= Priorities[ops.Peek()])
                PushExpression(ops, polish);
            
            ops.Push(token);
            isOpenParenthesis = true;
        }

        while (ops.Count > 0)
            PushExpression(ops, polish);

        return polish.Pop();
    }

    private static void PushExpression(Stack<string> operations, Stack<string> polish)
    {
        var op = operations.Pop();
        var val1 = polish.Pop();
        var val2 = polish.Pop();
        polish.Push(val2 + " " + val1 + " " + op);
    }
}