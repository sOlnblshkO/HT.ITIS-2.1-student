namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out CalculatorOperation operation,
        out double val2)
    {
        if (!IsArgLengthSupported(args))
            throw new ArgumentException("Incorrect input");

        val1 = FromStringToDouble(args[0]);
        operation = ParseOperation(args[1]);
        val2 = FromStringToDouble(args[2]);
    }

    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "/" => CalculatorOperation.Divide,
            "*" => CalculatorOperation.Multiply,
            _ => throw new InvalidOperationException("Incorrect input")
        };
    }

    private static double FromStringToDouble(string arg)
    {
        if (double.TryParse(arg, out var value))
            return value;
        else
            throw new ArgumentException("Value is not correct");
    }
}