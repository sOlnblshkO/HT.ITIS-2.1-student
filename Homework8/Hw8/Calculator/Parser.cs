using System.Globalization;

namespace Hw8.Calculator;

public class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out Operation operation,
        out double val2)
    {
        if (double.TryParse(args[0],NumberStyles.Any, CultureInfo.InvariantCulture,  out val1) && double.TryParse(args[2],NumberStyles.Any, CultureInfo.InvariantCulture,  out val2) ) //
        {
            operation = ParseOperation(args[1]);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private static Operation ParseOperation(string arg)
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