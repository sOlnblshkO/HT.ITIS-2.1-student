namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out CalculatorOperation operation,
        out double val2)
    {
        if (!IsArgLengthSupported(args))
        {
            throw new ArgumentException("There must be 3 arguments!");
        }

        val1 = ParseArgument(args[0]);
        val2 = ParseArgument(args[2]);
        operation = ParseOperation(args[1]);
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
            _ => throw new InvalidOperationException("Incorrect input operation")
        };
    }

    private static double ParseArgument(string arg)
    {

        if (Double.TryParse(arg, out var number))
        {
            return number;
        }

        throw new ArgumentException("Incorrect input number");
    }
}