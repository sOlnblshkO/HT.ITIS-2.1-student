using Hw10.ErrorMessages;

namespace Hw10.Services.MathCalculator;

public class MathExpressionValidatorService
{
    private static readonly string[] Operatinos = { "+", "-", "*", "/" };

    public static void ValidateExpression(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new Exception(MathErrorMessager.EmptyString);
        if (!expression.StartsWith('-') && Operatinos.Contains($"{expression[0]}"))
            throw new Exception(MathErrorMessager.StartingWithOperation);
        if (Operatinos.Contains($"{expression[^1]}"))
            throw new Exception(MathErrorMessager.EndingWithOperation);
        if (!CheckParenthesis(expression))
            throw new Exception(MathErrorMessager.IncorrectBracketsNumber);

        var symbols = expression.Split(" ");
        var prev = string.Empty;

        foreach (var s in symbols)
        {
            if (s.StartsWith('(')
                && Operatinos.Contains(s[1].ToString())
                && !s[1].Equals('-'))
                throw new Exception(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(s[1].ToString()));

            if (s.EndsWith(')')
                && Operatinos.Contains(s[^2].ToString()))
                throw new Exception(MathErrorMessager.OperationBeforeParenthesisMessage(s[^2].ToString()));

            var pure = s.Replace("(", "").Replace(")", "");

            if (!Operatinos.Contains(pure)
                && !double.TryParse(pure, out var num))
            {
                foreach (var c in pure.Where(c => !char.IsDigit(c)
                                                  && !c.Equals('.')
                                                  && !c.Equals('(')
                                                  && !c.Equals(')')
                                                  && !Operatinos.Contains(c.ToString())))
                    throw new Exception(MathErrorMessager.UnknownCharacterMessage(c));

                throw new Exception(MathErrorMessager.NotNumberMessage(s));
            }

            if (string.IsNullOrEmpty(prev))
            {
                prev = s;
                continue;
            }

            if (Operatinos.Contains(prev) && Operatinos.Contains(pure))
                throw new Exception(MathErrorMessager.TwoOperationInRowMessage(prev, pure));

            prev = s;
        }
    }

    private static bool CheckParenthesis(string expression)
    {
        var count = 0;
        foreach (var c in expression)
        {
            if (c.Equals('(')) count++;
            if (c.Equals(')')) count--;
            if (count < 0) return false;
        }

        return count == 0;
    }
}