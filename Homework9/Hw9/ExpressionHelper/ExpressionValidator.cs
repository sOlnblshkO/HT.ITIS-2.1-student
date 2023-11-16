using System.Text.RegularExpressions;
using Hw9.ErrorMessages;

namespace Hw9.ExpressionHelper;

public static class ExpressionValidator
{
    private static readonly char[] Operators = new char[] { '+', '-', '*', '/' };

    public static string Validate(string expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            return MathErrorMessager.EmptyString;
        }
        
        var expressionWithoutEmpties = expression.Replace(" ", "");
        
        if (Operators.Contains(expressionWithoutEmpties[0]))
        {
            return MathErrorMessager.StartingWithOperation;
        }
        if (Operators.Contains(expressionWithoutEmpties[^1]))
        {
            return MathErrorMessager.EndingWithOperation;
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
                return MathErrorMessager.IncorrectBracketsNumber;
            }
        }
        
        if (bracketsCount != 0)
        {
            return MathErrorMessager.IncorrectBracketsNumber;
        }
        
        foreach (var letter in expressionWithoutEmpties.Where(letter => !char.IsDigit(letter) && letter != '(' && letter != ')' && letter != '.' && !Operators.Contains(letter)))
        {
            return MathErrorMessager.UnknownCharacterMessage(letter);
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (Operators.Contains(expressionWithoutEmpties[i]) && Operators.Contains(expressionWithoutEmpties[i + 1]))
            {
                return MathErrorMessager.TwoOperationInRowMessage(expressionWithoutEmpties[i].ToString(), expressionWithoutEmpties[i+1].ToString());
            }
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (expressionWithoutEmpties[i] == '(' && Operators.Contains(expressionWithoutEmpties[i + 1]) && expressionWithoutEmpties[i + 1] != '-')
            {
                return MathErrorMessager.InvalidOperatorAfterParenthesisMessage(expressionWithoutEmpties[i + 1].ToString());
            }
        }
        
        for (var i = 0; i < expressionWithoutEmpties.Length - 1; i++)
        {
            if (expressionWithoutEmpties[i+1] == ')' && Operators.Contains(expressionWithoutEmpties[i]))
            {
                return MathErrorMessager.OperationBeforeParenthesisMessage(expressionWithoutEmpties[i].ToString());
            }
        }
        
        var regex = new Regex(@"\d+\.?\d*\/0");
        if (regex.IsMatch(expressionWithoutEmpties))
        {
            return MathErrorMessager.DivisionByZero;
        }
        
        regex = new Regex(@"\d+\.\d+\.\d+");

        var match = regex.Match(expressionWithoutEmpties);

        return match.Success ? MathErrorMessager.NotNumberMessage(match.Value) : "OK";
    }
    
    

}