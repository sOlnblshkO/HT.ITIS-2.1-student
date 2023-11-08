using System.Globalization;
using Hw8.Calculator;

namespace Hw8.Parser;

public class Parser : IParser
{
    public bool ParseArgument(string input, out double val)
    {
        return double.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out val);
    }

    public Operation ParseOperation(string operation)
    {
        return operation switch
        {
            "Plus" => Operation.Plus,
            "Minus" => Operation.Minus,
            "Multiply" => Operation.Multiply,
            "Divide" => Operation.Divide,
            _ => Operation.Invalid
        };
    }
}