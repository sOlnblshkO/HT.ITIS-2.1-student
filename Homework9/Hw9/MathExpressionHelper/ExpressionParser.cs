using System.Globalization;
using System.Text;
using Hw9.Extensions;

namespace Hw9.MathExpressionHelper;

public class ExpressionParser
{
    private static readonly Dictionary<string, int> OperationPriority;

    static ExpressionParser()
    {
       OperationPriority = new() {
            {"(", 0},
            {"+", 1},
            {"-", 1},
            {"*", 2},
            {"/", 2},
            {"~", 3}	//	Унарный минус
        };
    }
    public string ToPolishNotation(string expression)
    {
        var expressionWithoutSpaces = expression.WithoutSpaces();
        var expressionWithUnaryMinuses = ReplaceUnaryMinuses(expressionWithoutSpaces);
        var expressionTokens = SplitExpressionByTokens(expressionWithUnaryMinuses);
        
        //	Выходная строка, содержащая постфиксную запись
        var resultBuilder = new StringBuilder();
        var operationStack = new Stack<string>();

        foreach (var token in expressionTokens)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _)) resultBuilder.Append($"{token} ");
            else if (ExpressionValidator.IsOpeningBracket(token)) operationStack.Push(token);
            else if (ExpressionValidator.IsClosingBracket(token))
            {
                //	Заносим в выходную строку из стека всё вплоть до открывающей скобки
                while (operationStack.Any() && !ExpressionValidator.IsOpeningBracket(operationStack.Peek()))
                    resultBuilder.Append(operationStack.Pop() + " ");
                
                //	Удаляем открывающуюся скобку из стека
                operationStack.TryPop(out _);
            }
            else if (OperationPriority.TryGetValue(token, out var operation))
            {
                //	Заносим в выходную строку все операторы из стека, имеющие более высокий приоритет
                while (operationStack.Count > 0 && ( OperationPriority[operationStack.Peek()] >= operation))
                    resultBuilder.Append(operationStack.Pop() + " ");
                
                //	Заносим в стек оператор
                operationStack.Push(token);
            }
        }

        //	Заносим все оставшиеся операторы из стека в выходную строку
        foreach (var operation in operationStack)
            resultBuilder.Append(operation + " ");

        return resultBuilder.ToString();
    }

    private static string ReplaceUnaryMinuses(string expression)
    {
        var expressionBuilder = new StringBuilder();

        expressionBuilder.Append(expression[0] == '-' ? "~" : expression[0]);
        
        for (int i = 1; i < expression.Length; i++)
        {
            if (expression[i] == '-' && !ExpressionValidator.IsClosingBracket(expression[i-1]) 
                                     && !ExpressionValidator.IsPartOfNumber(expression[i-1]))
                expressionBuilder.Append("~");
            else 
                expressionBuilder.Append(expression[i]);
        }

        return expressionBuilder.ToString();
    }

    private static string[] SplitExpressionByTokens(string expression)
    {
        var result = expression;
        
        foreach (var operation in ExpressionValidator.Operations)
            result = result.Replace(operation, $" {operation} ");
       
        result = result.Replace("~", " ~ ");

        foreach (var bracket in ExpressionValidator.Brackets)
            result = result.Replace(bracket, $" {bracket} ");

        return result.Split().Without("").ToArray();
    }
}