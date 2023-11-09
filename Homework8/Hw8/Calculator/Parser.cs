using System.Globalization;

namespace Hw8.Calculator;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out Operation operation, 
        out double val2)
    {
        val1 = ParseNumber(args[0]);
        val2 = ParseNumber(args[2]);
        operation = ParseOperation(args[1]);
    }

    private static double ParseNumber(string number)
    {
        if (double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            return result;
        throw new ArgumentException($"Введите числа");
    }

    private static Operation ParseOperation(string arg) =>
        arg.ToLower() switch
        {
            "plus" => Operation.Plus,
            "minus" => Operation.Minus,
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => throw new InvalidOperationException($"Недопустимая операция. Допустимые: plus, minus," +
                                                     $" multiply и divide")
        };
}