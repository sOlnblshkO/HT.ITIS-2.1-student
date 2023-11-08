using System.Globalization;

namespace Hw8.Calculator;

public class Parser
{
    public static bool ParseToDouble(string input, out double val)
    {
        return double.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out val);
    }

    public static Operation ParseOperation(string arg)
    {
        return arg switch
        {
            "Plus" => Operation.Plus,
            "Minus" => Operation.Minus,
            "Multiply" => Operation.Multiply,
            "Divide" => Operation.Divide,
            _ => Operation.Invalid
        };
    }
}