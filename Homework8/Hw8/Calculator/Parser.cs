using System.Globalization;

namespace Hw8.Calculator;

public static class Parser
{
    public static void ParseOperation(string arg, out Operation operation)
    {
        operation = arg.ToLower() switch
        {
            "plus" => Operation.Plus,
            "minus" => Operation.Minus,
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }

    public static void ParseArgs(string arg1, string arg2, out double val1, out double val2)
    {
        if (!double.TryParse(arg1, NumberStyles.Float, CultureInfo.InvariantCulture, out val1) 
            || !double.TryParse(arg2, NumberStyles.Float, CultureInfo.InvariantCulture, out val2))
            throw new ArgumentException(Messages.InvalidNumberMessage);
    }
}