using Hw9.ErrorMessages;

namespace Hw9.Services.MathCalculator;

public class ExpressionValidator
{
    //только частично валидирует строку, то что должно вычисляться динамически(/(1-1) и /0 туда же) он не валидирует.
    private static readonly char[] ValidChars =
        { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/', '+', '-', '*', '(', ')', '.' };

    private static readonly char[] Separators = { '/', '+', '-', '*', '(', ')' };
    private static readonly char[] Operations = { '/', '+', '-', '*' };

    public static string? Validate(string? expression)
    {
        expression = expression?.Replace(" ", "");

        if (IsNull(expression))
        {
            return MathErrorMessager.EmptyString;
        }

        var invalidChar = ContainsInvalidChar(expression!);
        if (invalidChar is not "")
        {
            return MathErrorMessager.UnknownCharacterMessage(char.Parse(invalidChar!));
        }

        var invalidNumber = ContainsNotNumber(expression!);
        if (invalidNumber != null)
        {
            return MathErrorMessager.NotNumberMessage(invalidNumber);
        }

        var invalidOperations = ContainsTwoOperationsInRow(expression!);
        if (invalidOperations != null)
        {
            return MathErrorMessager.TwoOperationInRowMessage(invalidOperations.Split(" ")[0],
                invalidOperations.Split(" ")[1]);
        }

        var invalidOperationAfterParenthesis = ContainsInvalidOperatorAfterParenthesis(expression!);
        if (invalidOperationAfterParenthesis != null)
        {
            return MathErrorMessager.InvalidOperatorAfterParenthesisMessage(invalidOperationAfterParenthesis);
        }

        var invalidOperationBeforeParenthesis = ContainsOperationBeforeParenthesis(expression!);
        if (invalidOperationBeforeParenthesis != null)
        {
            return MathErrorMessager.OperationBeforeParenthesisMessage(invalidOperationBeforeParenthesis);
        }

        if (StartsWithOperator(expression!))
        {
            return MathErrorMessager.StartingWithOperation;
        }

        if (EndsWithOperator(expression!))
        {
            return MathErrorMessager.EndingWithOperation;
        }

        if (IsInvalidBracketNumber(expression!))
        {
            return MathErrorMessager.IncorrectBracketsNumber;
        }

        return null;
    }

    private static bool IsNull(string? expression)
    {
        return expression is "" or null;
    }

    private static string ContainsInvalidChar(string expression)
    {
        var resp = "";

        foreach (var c in expression)
        {
            if (!ValidChars.Contains(c))
            {
                resp = c.ToString();
            }
        }

        return resp;
    }

    private static string? ContainsNotNumber(string expression)
    {
        var stringChars = expression.Split(Separators);
        return stringChars
            .Where(x => !string.IsNullOrEmpty(x))
            .FirstOrDefault(s => int.TryParse(s, out _) == false);
    }

    private static string? ContainsTwoOperationsInRow(string expression)
    {
        int operationInRowCount = 0;
        char lastOperation = 'e'; //заглушка, никогда не выведется, просто нужно для компилятора

        foreach (var character in expression)
        {
            if (Operations.Contains(character))
            {
                operationInRowCount += 1;
                if (operationInRowCount == 2)
                {
                    return $"{lastOperation} {character}";
                }

                lastOperation = character;
            }
            else
            {
                operationInRowCount = 0;
            }
        }

        return null;
    }

    private static string? ContainsInvalidOperatorAfterParenthesis(string expression)
    {
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] is '(' && Operations.Contains(expression[i + 1]) && expression[i + 1] is not '-')
            {
                return expression[i + 1].ToString();
            }
        }

        return null;
    }

    private static string? ContainsOperationBeforeParenthesis(string expression)
    {
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] is ')' && Operations.Contains(expression[i - 1]))
            {
                return expression[i - 1].ToString();
            }
        }

        return null;
    }

    private static bool EndsWithOperator(string expression) => Operations.Contains((expression[^1]));

    private static bool StartsWithOperator(string expression) => Operations.Contains((expression[0]));

    private static bool IsInvalidBracketNumber(string expression)
    {
        int count = 0;

        foreach (char c in expression)
        {
            switch (c)
            {
                case '(':
                    count++;
                    break;
                case ')':
                    count--;
                    break;
            }

            if (count < 0)
            {
                return true;
            }
        }

        return count != 0;
    }
}