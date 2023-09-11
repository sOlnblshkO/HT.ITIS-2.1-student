namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out CalculatorOperation operation,
        out double val2)
    {
        if (!IsArgTypeSupported(args[0], args[2], out val1, out val2) ||
            !IsArgLengthSupported(args))
        {
            throw new ArgumentException("Inappropriate array format");
        }

        operation = ParseOperation(args[1]);
    }

    private static bool IsArgTypeSupported(string arg1, string arg2, out double val1, out double val2)
    {
        val2 = 0;
        return double.TryParse(arg1, out val1) && double.TryParse(arg2, out val2);
    }


    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => throw new InvalidOperationException("Unavailable operation")
        };
    }
}