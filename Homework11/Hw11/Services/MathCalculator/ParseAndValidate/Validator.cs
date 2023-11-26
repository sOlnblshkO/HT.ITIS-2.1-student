using System.Text.RegularExpressions;
using Hw11.ErrorMessages;

namespace Hw11.Services.MathCalculator.ParseAndValidate;



public class Validator
{
    static private readonly string[] operations = new string[] { "/", "*", "-", "+" };

    public static void Validate(string input)
    {
        
        if (string.IsNullOrEmpty(input))
            throw new Exception(MathErrorMessager.EmptyString);
        
        if (!CheckCorrectParenthesis(input))
            throw new Exception(MathErrorMessager.IncorrectBracketsNumber);
        
        if (operations.Contains(input[0].ToString()) && input[0] != '-')
            throw new Exception(MathErrorMessager.StartingWithOperation);

        if (operations.Contains(input[input.Length - 1].ToString()))
            throw new Exception(MathErrorMessager.EndingWithOperation);
        
        foreach (var i in input.Where(c => !char.IsDigit(c) && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(c)))
            throw new Exception(MathErrorMessager.UnknownCharacterMessage(i));
        var array = new Regex("(?<=[-+*/()])|(?=[-+*/()])").Split(input.Replace(" ", ""))
            .Where(c => c != "").ToArray();
        
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Length > 1 && !double.TryParse(array[i], out _))
                throw new Exception(MathErrorMessager.NotNumberMessage(array[i]));
            
            if (i > 0 && operations.Contains(array[i]) && operations.Contains(array[i - 1]))
                throw new Exception(MathErrorMessager.TwoOperationInRowMessage(array[i - 1], array[i]));
            
            if (array[i] == "(" && !Double.TryParse(array[i + 1], out _) && array[i + 1] != "-")
                throw new Exception(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(array[i + 1]));
            
            if (array[i] == ")" && !Double.TryParse(array[i - 1], out _) && operations.Contains(array[i - 1]))
                throw new Exception(MathErrorMessager.OperationBeforeParenthesisMessage(array[i - 1]));
        }
    }

    private static bool CheckCorrectParenthesis(string input)
    {
        var stack = new Stack<char>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                stack.Push(input[i]);
            }
            else if (input[i] == ')')
            {
                if (stack.Count == 0)
                    return false;

                stack.Pop();
            }
        }

        return stack.Count == 0;
    }
}