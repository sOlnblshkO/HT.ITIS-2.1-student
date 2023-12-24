using System.Globalization;
using Hw11.ErrorMessages;
using Hw11.Exceptions;
using Hw11.Extensions;

namespace Hw11.MathExpressionHelper;
/// <summary>
/// Класс отвечающий за проверку на корректность арифметического выражения
/// </summary>
public static class ExpressionValidator
{
    private static readonly char[] AcceptedCharacters;
    private static readonly string[] UnaryOperations;
    public static readonly string[] Operations;
    public static readonly string[] Brackets;
    public const string Correct = "Correct";
    
    static ExpressionValidator()
    {
        UnaryOperations = new[] { "-" };
        AcceptedCharacters = new[] { '.', ',' };
        Operations = new[] { "+", "-", "*", "/" };
        Brackets = new[] { "(", ")" };
    }
    
    /// <summary>
    /// Проверяет арифметическое выражение на корректность
    /// </summary>
    /// <param name="expression">Арифметическое выражение</param>
    /// <returns>Correct или MathErrorMessage</returns>
    public static void CheckForCorrectExpression(string? expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) throw new Exception(MathErrorMessager.EmptyString);

        var expressionWithoutSpaces = expression.WithoutSpaces();
        
        CheckForBracketsSequenceCorrect(expressionWithoutSpaces);
        
        CheckForUnknownCharacters(expressionWithoutSpaces);
        
        CheckForOperationsSemantic(expressionWithoutSpaces);
        
        CheckForCorrectArguments(expressionWithoutSpaces);
    }

    /// <summary>
    /// Проверяет аргументы на корректную запись
    /// </summary>
    /// <param name="expression">Арифметическое выражение без пробелов</param>
    /// <returns>Correct или MathErrorMessage</returns>
    private static void CheckForCorrectArguments(string expression)
    {
        var numbers = expression.WithoutBrackets().
            Replace(Operations, " ").
            Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var number in numbers)
            if (!double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                throw new InvalidNumberException(MathErrorMessager.NotNumberMessage(number));
    }

    /// <summary>
    /// Проверяет правильность скобочной последовательности в выражении
    /// </summary>
    /// <param name="expression">Арифметическое выражение без пробелов</param>
    /// <returns>Correct или MathErrorMessage</returns>
    private static void CheckForBracketsSequenceCorrect(string expression)
    {
        var bracketsStack = new Stack<char>();
        foreach (var symbol in expression)
        {
            if (!IsBracket(symbol)) continue;
            
            if(IsOpeningBracket(symbol)) bracketsStack.Push(symbol);
            else if (!bracketsStack.TryPop(out _)) throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
        }

        if (bracketsStack.Any())
            throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
    }

    /// <summary>
    /// Проверяет правильность расстановки операций в выражении
    /// </summary>
    /// <param name="expression">Арифметическое выражение без пробелов</param>
    /// <returns>Correct или MathErrorMessage</returns>
    private static void CheckForOperationsSemantic(string expression)
    {
        var startsWithOperation = expression.StartsWith(Operations.Without(UnaryOperations));
        var endsWithOperation = expression.EndsWith(Operations);

        if (startsWithOperation)  throw new InvalidSymbolException(MathErrorMessager.StartingWithOperation);
        if (endsWithOperation) throw new InvalidSymbolException(MathErrorMessager.EndingWithOperation);
        
        for (int i = 1; i < expression.Length-1; i++)
        {
            if (IsOperation(expression[i]) && IsOperation(expression[i+1]))
                throw new InvalidSymbolException(MathErrorMessager.TwoOperationInRowMessage(expression[i].ToString(), expression[i+1].ToString()));
            
            if (IsClosingBracket(expression[i+1]) && IsOperation(expression[i]))
                throw new InvalidSymbolException(MathErrorMessager.OperationBeforeParenthesisMessage(expression[i].ToString()));

            if (IsOpeningBracket(expression[i]) && IsOperation(expression[i + 1]) && expression[i + 1] != '-')
                throw new InvalidSymbolException(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(expression[i + 1].ToString()));
        }
    }

    /// <summary>
    /// Проверяет наличие неизвестных символов
    /// </summary>
    /// <param name="expression">Арифметическое выражение без пробелов</param>
    /// <returns>Correct или MathErrorMessage</returns>
    private static void CheckForUnknownCharacters(string expression)
    {
        var formatExpression = expression;
        var unknownCharacters = formatExpression.Where(character => !IsBracket(character) 
                                                                   && !IsPartOfNumber(character)
                                                                   && !IsOperation(character)).ToArray();

        if(unknownCharacters.Length != 0)
            throw new InvalidSymbolException(MathErrorMessager.UnknownCharacterMessage(unknownCharacters.First()));
    }
    
    /// <summary>
    /// Проверяет, является ли символ скобкой ( или )
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - ( или ), иначе false</returns>
    public static bool IsBracket(char symbol) => Brackets.Contains(symbol.ToString());

    
    /// <summary>
    /// Проверяет, является ли символ скобкой цифрой, или точкой, или запятой 
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - [0-9], или '.', ',' или , иначе false</returns>
    public static bool IsPartOfNumber(char symbol) => char.IsDigit(symbol) || AcceptedCharacters.Contains(symbol);

    /// <summary>
    /// Проверяет, является ли символ одной из операций: '+', '-', '*', '/' 
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - '+', '-', '*' или '/' иначе false</returns>
    public static bool IsOperation(char symbol) => Operations.Contains(symbol.ToString());
    
    /// <summary>
    /// Проверяет, является ли строка одной из операций: "+", "-", "*", "/"
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - "+", "-", "*" или "/" иначе false</returns>
    public static bool IsOperation(string symbol) => Operations.Contains(symbol);

    /// <summary>
    /// Проверяет, является ли символ открывающей скобкой '('
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - '(', иначе false</returns>
    public static bool IsOpeningBracket(char symbol) => symbol == '(';

    /// <summary>
    /// Проверяет, является ли символ закрывающей скобкой ')'
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если символ - ')', иначе false</returns>
    public static bool IsClosingBracket(char symbol) => symbol == ')';
    
    /// <summary>
    /// Проверяет, является ли строка открывающей скобкой "("
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если строка - "(", иначе false</returns>
    public static bool IsOpeningBracket(string symbol) => symbol[0] == '(';

    /// <summary>
    /// Проверяет, является ли строка закрывающей скобкой ")"
    /// </summary>
    /// <param name="symbol">Символ</param>
    /// <returns>true - если строка - ")", иначе false</returns>
    public static bool IsClosingBracket(string symbol) => symbol[0] == ')';

}