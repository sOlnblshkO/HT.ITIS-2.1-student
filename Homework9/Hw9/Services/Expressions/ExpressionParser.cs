using static Hw9.Services.Patterns;

namespace Hw9.Services.Expressions;

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
        var openBrackets = new Stack<string>();
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
                    openBrackets.Push(token);
                    isOpenParenthesis = true;
                    continue;
                case ")":
                {
                    while (openBrackets.Peek() != "(")
                        PushExpression(openBrackets, polish);
                    openBrackets.Pop();
                    isOpenParenthesis = false;
                    continue;
                }
            }

            while (openBrackets.Count > 0 && Priorities[token] <= Priorities[openBrackets.Peek()])
                PushExpression(openBrackets, polish);
            
            openBrackets.Push(token);
            isOpenParenthesis = true;
        }

        while (openBrackets.Count > 0)
            PushExpression(openBrackets, polish);

        return polish.Pop();
    }

    private static void PushExpression(Stack<string> operations, Stack<string> polish)
    {
        var operation = operations.Pop();
        var val1 = polish.Pop();
        var val2 = polish.Pop();
        
        polish.Push(val2 + " " + val1 + " " + operations);
    }
}