using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw9.ErrorMessages;
using Hw9.Services.ExpressionTokenizer;

namespace Hw9.Services.ExpressionParser;

public class MathExpressionParser : IExpressionParser
{
    private static readonly Dictionary<string, int> OperatorsPriorities =
        new() { { "+", 0 }, { "-", 0 }, { "*", 1 }, { "/", 1 }, { "0-", 2 }, { "(", 3 }, { ")", 3 } };


    private readonly IExpressionTokenizer _tokenizer;

    public MathExpressionParser(IExpressionTokenizer tokenizer)
    {
        _tokenizer = tokenizer;
    }

    public Expression Parse(string expression)
    {
        var tokens = _tokenizer.Tokenize(expression);
        var opStack = new Stack<string>();
        var resStack = new Stack<Expression>();
        var isLastTokenOperation = false;
        var lastToken = "";
        var checkedStart = false;

        foreach (var token in tokens)
        {
            if (!checkedStart)
            {
                if (new[] { "+", "-", "*", "/", ")" }.Contains(token))
                    throw new ArgumentException(MathErrorMessager.StartingWithOperation);
                checkedStart = true;
            }

            if (OperatorsPriorities.ContainsKey(token))
            {
                if (isLastTokenOperation && token != "(" && lastToken != ")" && !(lastToken == "(" && token == "0-"))
                {
                    if (token == ")")
                        throw new ArgumentException(
                            MathErrorMessager.OperationBeforeParenthesisMessage(lastToken));
                    if (lastToken == "(")
                        throw new ArgumentException(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(token));
                    throw new ArgumentException(MathErrorMessager.TwoOperationInRowMessage(lastToken, token));
                }

                while (opStack.Count > 0 && opStack.Peek() != "(" &&
                       (OperatorsPriorities[opStack.Peek()] >= OperatorsPriorities[token] || token == ")"))
                    PushOperatorToExpressionStack(opStack.Pop(), resStack);
                if (token == ")")
                {
                    if (opStack.Count == 0 || opStack.Peek() != "(")
                        throw new ArgumentException(MathErrorMessager.IncorrectBracketsNumber);
                    opStack.Pop();
                }
                else
                {
                    opStack.Push(token);
                }

                isLastTokenOperation = true;
            }
            else
            {
                var parsed = double.TryParse(token, out var number);
                if (!parsed)
                    throw new ArgumentException(MathErrorMessager.NotNumberMessage(token));

                resStack.Push(Expression.Constant(number));
                isLastTokenOperation = false;
            }

            while (opStack.Count > 0 && token != "0-" && opStack.Peek() == "0-")
                PushOperatorToExpressionStack(opStack.Pop(), resStack);

            lastToken = token;
        }

        while (opStack.Count > 0)
        {
            if (opStack.Peek() == "(")
                throw new ArgumentException(MathErrorMessager.IncorrectBracketsNumber);
            PushOperatorToExpressionStack(opStack.Pop(), resStack);
        }

        return resStack.Pop();
    }

    [ExcludeFromCodeCoverage]
    public static void PushOperatorToExpressionStack(string op, Stack<Expression> expressionStack)
    {
        if ((op == "0-" && expressionStack.Count == 0) || (op != "0-" && expressionStack.Count < 2))
            throw new ArgumentException(MathErrorMessager.EndingWithOperation);

        var rightOperand = expressionStack.Pop();
        expressionStack.Push(op switch
        {
            "+" => Expression.Add(expressionStack.Pop(), rightOperand),
            "-" => Expression.Subtract(expressionStack.Pop(), rightOperand),
            "*" => Expression.Multiply(expressionStack.Pop(), rightOperand),
            "/" => Expression.Divide(expressionStack.Pop(), rightOperand),
            "0-" => Expression.Negate(rightOperand),
            _ => throw new ArgumentException()
        });
    }
}