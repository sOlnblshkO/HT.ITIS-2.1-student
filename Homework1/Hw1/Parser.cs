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
            throw new ArgumentException();
        }

        if (ParseOperation(args[1]) == CalculatorOperation.Undefined)
        {
            throw new InvalidOperationException();
        }

        try
        {
            val1 = int.Parse(args[0]);
            operation = ParseOperation(args[1]);
            val2 = int.Parse(args[2]);
        }
        catch (FormatException)
        {
            throw new ArgumentException();
        }
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
            _ => CalculatorOperation.Undefined
        };
    }
}