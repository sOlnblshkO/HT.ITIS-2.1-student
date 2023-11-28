using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Hw11.Services.ExpressionParser;

internal enum TokenType
{
    Number,
    Operation,
    Parenthesis
}

internal class Token
{
    internal string? Value { get; set; }

    internal TokenType? Type { get; set; }
}

public class ExpressionParser : IExpressionParser
{
    private readonly Regex _numbers = new(@"^\d+");
    private readonly Regex _delimiters = new("(?<=[-+*/()])|(?=[-+*/()])");
    private readonly Dictionary<string, int> _operatorPriorities = new()
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };

    public Expression Parse(string expression)
    {
        var postfixNotation = ConvertToPostfixNotation(expression);
        var partedExpression = PartitionPostfixNotation(postfixNotation);
        return CreateExpression(partedExpression);
    }
        private string ConvertToPostfixNotation(string infixExpression)
        {
            var ops = new Stack<string>();
            var result = new Stack<string>();
            var inputSplitted = _delimiters.Split(infixExpression.Replace(" ", ""));
    
            var isLastTokenOp = true;
            for (var i = 0; i < inputSplitted.Length; i++)
            {
                var token = inputSplitted[i];
                if (token.Length == 0) continue;
                if (_numbers.IsMatch(token))
                {
                    result.Push(token);
                    isLastTokenOp = false;
                    continue;
                }
                switch (token)
                {
                    case "-" when isLastTokenOp:
                        result.Push(token + inputSplitted[++i]);
                        isLastTokenOp = false;
                        continue;
                    case "(":
                        ops.Push(token);
                        isLastTokenOp = true;
                        continue;
                    case ")":
                    {
                        while (ops.Peek() != "(")
                            PushOperation(ops, result);
                        ops.Pop();
                        isLastTokenOp = false;
                        continue;
                    }
                }
                while (ops.Count > 0 && _operatorPriorities[token] <= _operatorPriorities[ops.Peek()])
                    PushOperation(ops, result);
                ops.Push(token);
                isLastTokenOp = true;
            }

            while (ops.Count > 0)
                PushOperation(ops, result);
            return result.Pop();
        }

    private List<Token> PartitionPostfixNotation(string expression)
    {
        var list = new List<Token>();
        foreach (var s in expression.Split(" "))
        {
            var part  = new Token {Value = s};
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                part.Type = TokenType.Number;

            else
                part.Type = TokenType.Operation;
            list.Add(part);
        }
        return list;
    }

    private Expression CreateExpression(List<Token> tokens)
    {
        var buffer = new Stack<Expression>();
        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                {
                    buffer.Push(Expression.Constant(double.Parse(token.Value!, NumberStyles.Float, CultureInfo.InvariantCulture)));
                    break;
                }
                case TokenType.Operation:
                {
                    Func<Expression, Expression, Expression> expr = token.Value switch
                    {
                        "/" => Expression.Divide,
                        "*" => Expression.Multiply,
                        "-" => Expression.Subtract,
                        _ => Expression.Add
                    };
                    Expression first = buffer.Pop(), second = buffer.Pop();
                    buffer.Push(expr(second, first));
                    break;
                }
            }
        }
        return buffer.Pop();
    }

    private void PushOperation(Stack<string> operations, Stack<string> polish)
    {
        var op = operations.Pop();
        var v1 = polish.Pop();
        var v2 = polish.Pop();
        polish.Push($"{v2} {v1} {op}");
    }
}