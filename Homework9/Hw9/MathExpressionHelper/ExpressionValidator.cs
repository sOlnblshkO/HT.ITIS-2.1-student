using System.Globalization;
using Hw9.ErrorMessages;
using Hw9.Extensions;

namespace Hw9.MathExpressionHelper;

public static class ExpressionValidator
{
    public static readonly char[] AcceptedCharacters;
    public static readonly string[] Operations;
    public static readonly string[] Brackets;
    public const string Correct = "Correct";
    
    static ExpressionValidator()
    {
        AcceptedCharacters = new[] { '.', ',' };
        Operations = new[] { "+", "-", "*", "/" };
        Brackets = new[] { "(", ")" };
    }
    
    public static async Task<string> CheckForCorrectExpressionAsync(string? expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) return MathErrorMessager.EmptyString;
        if (expression.WithoutSpaces().Contains("/0")) return MathErrorMessager.DivisionByZero;

        var expressionWithoutSpaces = expression.WithoutSpaces();

        // var expressionValidationMethods = new List<Func<string, string>>()
        // {
        //     CheckForBracketsSequenceCorrect,
        //     CheckForUnknownCharacters,
        //     CheckForOperationsSemantic,
        //     CheckForCorrectArguments
        // };
        //
        // foreach (var method in expressionValidationMethods)
        // {
        //     var result = method.Invoke(expressionWithoutSpaces);
        //     if (!result.Equals(Correct))
        //         return result;
        // }
        
        var expressionValidateTasks = new List<Task<string>>
        {
            new(() => CheckForBracketsSequenceCorrect(expressionWithoutSpaces)),
            new(() => CheckForUnknownCharacters(expressionWithoutSpaces)),
            new(() => CheckForOperationsSemantic(expressionWithoutSpaces)),
            new(() => CheckForCorrectArguments(expressionWithoutSpaces))
        };
        
        foreach (var task in expressionValidateTasks)
            task.Start();
        
        var expressionValidateResultList  = await Task.WhenAll(expressionValidateTasks);
        
        foreach (var result in expressionValidateResultList)
            if (!result.Equals(Correct))
                return result;

        return Correct;
    }

    private static string CheckForCorrectArguments(string expression)
    {
        var numbers = expression.
            WithoutBrackets().
            Replace(Operations, " ").
            Split(" ")
            .Without("");

        foreach (var number in numbers)
            if (!double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                return MathErrorMessager.NotNumberMessage(number);
        
        return Correct;
    }

    private static string CheckForBracketsSequenceCorrect(string expression)
    {
        var bracketsStack = new Stack<char>();
        foreach (var symbol in expression)
        {
            if (!IsBracket(symbol)) continue;
            
            if(IsOpeningBracket(symbol)) bracketsStack.Push(symbol);
            else if (!bracketsStack.TryPop(out _)) return MathErrorMessager.IncorrectBracketsNumber;
        }

        return !bracketsStack.Any() ? Correct : MathErrorMessager.IncorrectBracketsNumber;
    }

    private static string CheckForOperationsSemantic(string expression)
    {
        var startsWithOperation = expression.StartsWith(Operations.Without("-"));
        var endsWithOperation = expression.EndsWith(Operations);

        if (startsWithOperation) return MathErrorMessager.StartingWithOperation;
        if (endsWithOperation) return MathErrorMessager.EndingWithOperation;
        
        for (int i = 1; i < expression.Length-1; i++)
        {
            if (IsOperation(expression[i]) && IsOperation(expression[i+1]))
                return MathErrorMessager.TwoOperationInRowMessage(expression[i].ToString(), expression[i+1].ToString());
            
            if (IsClosingBracket(expression[i+1]) && IsOperation(expression[i]))
                return MathErrorMessager.OperationBeforeParenthesisMessage(expression[i].ToString());

            if (IsOpeningBracket(expression[i]) && IsOperation(expression[i + 1]) && expression[i + 1] != '-')
                return MathErrorMessager.InvalidOperatorAfterParenthesisMessage(expression[i + 1].ToString());
        }

        return Correct;
    }

    private static string CheckForUnknownCharacters(string expression)
    {
        var formatExpression = expression;
        var unknownCharacters = formatExpression.Where(character => !IsBracket(character) 
                                                                   && !IsPartOfNumber(character)
                                                                   && !IsOperation(character)).ToArray();

        return unknownCharacters.Length == 0 ? Correct : MathErrorMessager.UnknownCharacterMessage(unknownCharacters.First());
    }

    public static bool IsBracket(char symbol) => Brackets.Contains(symbol.ToString());

    public static bool IsPartOfNumber(char symbol) => char.IsDigit(symbol) || AcceptedCharacters.Contains(symbol);

    public static bool IsOperation(char symbol) => Operations.Contains(symbol.ToString());
    
    public static bool IsOperation(string symbol) => Operations.Contains(symbol);

    public static bool IsOpeningBracket(char symbol) => symbol == '(';

    public static bool IsClosingBracket(char symbol) => symbol == ')';
    
    public static bool IsOpeningBracket(string symbol) => symbol[0] == '(';

    public static bool IsClosingBracket(string symbol) => symbol[0] == ')';

}