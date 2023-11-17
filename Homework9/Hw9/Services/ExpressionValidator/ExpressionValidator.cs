using Hw9.ErrorMessages;
using System.Globalization;

namespace Hw9.Services.ExpressionValidator;

public class ExpressionValidator : IExpressionValidator
{
    private readonly HashSet<char> ValidOperations = new() { '+', '-', '*', '/' };

    public void Validate(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new Exception(MathErrorMessager.EmptyString);
        var expr = expression.Replace(" ",""); //Expression without spaces
        if (IndexOfUnknownCharacter(expr) != -1)
            throw new Exception(MathErrorMessager.UnknownCharacterMessage(expr[IndexOfUnknownCharacter(expr)]));
        if (ValidOperations.Contains(expr[0]))
            throw new Exception(MathErrorMessager.StartingWithOperation);
        if (ValidOperations.Contains(expr[^1]))
            throw new Exception(MathErrorMessager.EndingWithOperation);
        if (FindOperationBeforeClosingParenthesis(expr) != "")
            throw new Exception(
                MathErrorMessager.OperationBeforeParenthesisMessage(FindOperationBeforeClosingParenthesis(expr)));
        if (FindOperationAfterOpenParenthesis(expr) != "" && FindOperationAfterOpenParenthesis(expr) != "-")
            throw new Exception(
                MathErrorMessager.InvalidOperatorAfterParenthesisMessage( FindOperationAfterOpenParenthesis(expr)));
        if (ContainsTwoOperationsInARow(expr, out var op1, out var op2))
            throw new Exception(MathErrorMessager.TwoOperationInRowMessage(op1.ToString()!, op2.ToString()!));
        if (!ValidParenthesis(expr))
            throw new Exception(MathErrorMessager.IncorrectBracketsNumber);
        if (ContainsNotNumber(expr, out var notNumber))
            throw new Exception(MathErrorMessager.NotNumberMessage(notNumber!));
    }

    private bool ValidParenthesis(string expression)
    {
        var stack = new Stack<char>();
        foreach (var ch in expression)
        {
            switch (ch)
            {
                case '(':
                    stack.Push(ch);
                    break;
                case ')' when stack.Count == 0 || stack.Peek() != '(':
                    return false;
                case ')':
                    stack.Pop();
                    break;
            }
        }
        return stack.Count == 0;
    }

    private bool ContainsTwoOperationsInARow(string expression, out char? op1, out char? op2)
    {
        op1 = op2 = null;
        var stack = new Stack<char>();
        foreach (var ch in expression)
        {
            if (ValidOperations.Contains(ch))
                if (stack.Count == 0)
                    stack.Push(ch);
                else
                {
                    op1 = stack.Pop();
                    op2 = ch;
                    return true;
                }
            else if ((char.IsDigit(ch) || ch == '(') && stack.Count != 0)
                stack.Pop();
        }
        return false;
    }

    private string FindOperationBeforeClosingParenthesis(string expression)
    {
        for(var i = 0; i < expression.Length - 1; i++)
            if (ValidOperations.Contains(expression[i]) && expression[i + 1] is ')')
                return expression[i].ToString();
        return "";
    }
    
    private string FindOperationAfterOpenParenthesis(string expression)
    {
        for(var i = 0; i < expression.Length - 1; i++)
            if (expression[i] is '(' && ValidOperations.Contains(expression[i+1]))
                return expression[i+1].ToString();
        return "";
    }
    
    private bool ContainsNotNumber(string expression, out string? notNumber)
    {
        notNumber = expression
            .Split('+', '-', '/', '*', '(', ')')
            .Where(x => !string.IsNullOrEmpty(x))
            .FirstOrDefault(x =>
                !double.TryParse(x, NumberStyles.Float, CultureInfo.InvariantCulture, out _));
        return notNumber is not null;
    }
    
    private int IndexOfUnknownCharacter(string expression)
    {
        var validCharacters = new HashSet<char> { '(', ')', '.', ',' };
        for (var i = 0; i < expression.Length; i++)
            if (!char.IsDigit(expression[i]) 
                && !ValidOperations.Contains(expression[i]) 
                &&  !validCharacters.Contains(expression[i]))
                return i;
        return -1;
    }
}