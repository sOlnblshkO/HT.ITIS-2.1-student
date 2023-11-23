using static Hw10.ErrorMessages.MathErrorMessager;
using static Hw10.Services.Patterns;

namespace Hw10.Services.Expressions;

public static class ExpressionValidator
{
    public static void Validate(string? expression, out string[] splittedExpression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new Exception(EmptyString);
        
        if (!CheckCorrectBracketsNumber(expression))
            throw new Exception(IncorrectBracketsNumber);
        
        splittedExpression = SmartSplitPattern.Split(expression.Replace(" ", ""))
            .Where(element => !string.IsNullOrWhiteSpace(element))
            .ToArray();
        
        foreach (var element in expression.Where(element => 
                     !NumPattern.IsMatch(element.ToString()) 
                     && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(element)))
            throw new Exception(UnknownCharacterMessage(element));
        
        if (!NumPattern.IsMatch(splittedExpression[0]) && !new[] { "-", "(" }.Contains(splittedExpression[0]))
            throw new Exception(StartingWithOperation);
        
        if (!NumPattern.IsMatch(splittedExpression[^1]) && splittedExpression[^1] != ")")
            throw new Exception(EndingWithOperation);

        CheckCorrectNestingInExpression(splittedExpression);
    }

    private static void CheckCorrectNestingInExpression(string[] expression)
    {
        var previousElement = "";
        var isInsideParenthesis = true;

        foreach (var character in expression)
        {
            if (NumPattern.IsMatch(character))
            {
                previousElement = character;
                isInsideParenthesis = false;
                if (!double.TryParse(character, out _))
                    throw new Exception(NotNumberMessage(character));
                continue;
            }

            if (character == "-" && isInsideParenthesis)
            {
                previousElement = character;
                isInsideParenthesis = false;
                continue;
            }

            switch (character)
            {
                case "(":
                    previousElement = character;
                    isInsideParenthesis = true;
                    continue;
                case ")":
                {
                    if (isInsideParenthesis)
                        throw new Exception(OperationBeforeParenthesisMessage(previousElement));
                    previousElement = character;
                    isInsideParenthesis = false;
                    continue;
                }
            }

            if (isInsideParenthesis)
            {
                if (previousElement == "(")
                    throw new Exception(InvalidOperatorAfterParenthesisMessage(character));
                throw new Exception(TwoOperationInRowMessage(previousElement, character));
            }

            previousElement = character;
            isInsideParenthesis = true;
        }
    }

    private static bool CheckCorrectBracketsNumber(string input)
    {
        var openedParenthesisCount = 0;
        foreach (var c in input)
            switch (c)
            {
                case '(':
                    openedParenthesisCount++;
                    break;
                case ')':
                    openedParenthesisCount--;
                    break;
            }

        return openedParenthesisCount == 0;
    }
}