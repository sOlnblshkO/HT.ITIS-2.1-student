using static Hw11.ErrorMessages.MathErrorMessager;
using static Hw11.Patterns.Patterns;

namespace Hw11.Services.Expressions;

public static class ExpressionValidator
{
    public static void Validate(string? expression, out string[] splittedExpression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new Exception(EmptyString);
        if (!CheckCorrectBracketsNumber(expression))
            throw new Exception(IncorrectBracketsNumber);
        splittedExpression = SmartSplitPattern.Split(expression.Replace(" ", ""))
            .Where(c => c is not "").ToArray();
        foreach (var element in expression)
            if(!NumPattern.IsMatch(element.ToString()) 
               && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(element))
                throw new Exception(UnknownCharacterMessage(element));
        if (!NumPattern.IsMatch(splittedExpression[0]) && !new[] { "-", "(" }.Contains(splittedExpression[0]))
            throw new Exception(StartingWithOperation);
        if (!NumPattern.IsMatch(splittedExpression[^1]) && splittedExpression[^1] != ")")
            throw new Exception(EndingWithOperation);

        var lastCharacter = "";
        var isOpenParenthesis = true;

        foreach (var character in splittedExpression)
        {
            if (NumPattern.IsMatch(character))
            {
                lastCharacter = character;
                isOpenParenthesis = false;
                if (!double.TryParse(character, out _))
                    throw new Exception(NotNumberMessage(character));
                continue;
            }

            if (character == "-" && isOpenParenthesis)
            {
                lastCharacter = character;
                isOpenParenthesis = false;
                continue;
            }

            switch (character)
            {
                case "(":
                    lastCharacter = character;
                    isOpenParenthesis = true;
                    continue;
                case ")":
                {
                    if (isOpenParenthesis)
                        throw new Exception(OperationBeforeParenthesisMessage(lastCharacter));
                    lastCharacter = character;
                    isOpenParenthesis = false;
                    continue;
                }
            }

            if (isOpenParenthesis)
            {
                if (lastCharacter == "(")
                    throw new Exception(InvalidOperatorAfterParenthesisMessage(character));
                throw new Exception(TwoOperationInRowMessage(lastCharacter, character));
            }

            lastCharacter = character;
            isOpenParenthesis = true;
        }
    }

    private static bool CheckCorrectBracketsNumber(string input)
    {
        var openedParenthesis = 0;
        foreach (var c in input)
            switch (c)
            {
                case '(':
                    openedParenthesis++;
                    break;
                case ')' when openedParenthesis == 0:
                    return false;
                case ')':
                    openedParenthesis--;
                    break;
            }

        return openedParenthesis == 0;
    }
}