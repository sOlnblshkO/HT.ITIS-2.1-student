using System.Text.RegularExpressions;
using Hw11.ErrorMessages;
using Hw11.Exceptions;

namespace Hw11.ExpressionHelper;

public static class ExpressionValidator
{
    private static readonly char[] Operators = new char[] { '+', '-', '*', '/' };

    public static bool Validate(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            throw new InvalidSyntaxException(MathErrorMessager.EmptyString);
        }
        
        var expressionWithoutEmpties = expression.Replace(" ", "");
        
        if (Operators.Contains(expressionWithoutEmpties[0]))
        {
            throw new InvalidSyntaxException(MathErrorMessager.StartingWithOperation);
        }
        if (Operators.Contains(expressionWithoutEmpties[^1]))
        {
            throw new InvalidSyntaxException(MathErrorMessager.EndingWithOperation);
        }
        
        var bracketsCount = 0;
        foreach (var c in expressionWithoutEmpties)
        {
            switch (c)
            {
                case '(':
                    bracketsCount++;
                    break;
                case ')':
                    bracketsCount--;
                    break;
            }
            
            if (bracketsCount < 0)
            {
                throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
            }
        }
        
        if (bracketsCount != 0)
        {
            throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
        }
        
        foreach (var letter in expressionWithoutEmpties.Where(letter => !char.IsDigit(letter) && letter != '(' && letter != ')' && letter != '.' && !Operators.Contains(letter)))
        {
            throw new InvalidSymbolException(MathErrorMessager.UnknownCharacterMessage(letter));
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (Operators.Contains(expressionWithoutEmpties[i]) && Operators.Contains(expressionWithoutEmpties[i + 1]))
            {
                throw new InvalidSyntaxException(MathErrorMessager.TwoOperationInRowMessage(expressionWithoutEmpties[i].ToString(), expressionWithoutEmpties[i+1].ToString()));
            }
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (expressionWithoutEmpties[i] == '(' && Operators.Contains(expressionWithoutEmpties[i + 1]) && expressionWithoutEmpties[i + 1] != '-')
            {
                throw new InvalidSyntaxException(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(expressionWithoutEmpties[i + 1].ToString()));
            }
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (expressionWithoutEmpties[i+1] == ')' && Operators.Contains(expressionWithoutEmpties[i]))
            {
                throw new InvalidSyntaxException(MathErrorMessager.OperationBeforeParenthesisMessage(expressionWithoutEmpties[i].ToString()));
            }
        }
        
        var regex = new Regex(@"\d+\.?\d*\/0");
        if (regex.IsMatch(expressionWithoutEmpties))
        {
            throw new DivideByZeroException(MathErrorMessager.DivisionByZero);
        }
        
        regex = new Regex(@"\d+\.\d+\.\d+");

        var match = regex.Match(expressionWithoutEmpties);

        return match.Success ? throw new InvalidNumberException(MathErrorMessager.NotNumberMessage(match.Value)) : true;
    }
}