namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (!int.TryParse(args[0], out _) || !int.TryParse(args[2], out _))
            throw new ArgumentException("Wrong args");
        IsArgLengthSupported(args);

        val1 = double.Parse(args[0]);
        val2 = double.Parse(args[2]);
        operation = ParseOperation(args[1]);
    }

    private static bool IsArgLengthSupported(string[] args)
    {
        if (args.Length == 3)
            return true;
        throw new ArgumentException("Wrong args length");
    }

    private static CalculatorOperation ParseOperation(string arg)
    {
        switch (arg)
        {
            case "+":
                {
                    return CalculatorOperation.Plus;
                }
            case "-":
                {
                    return CalculatorOperation.Minus;
                }
            case "*":
                {
                    return CalculatorOperation.Multiply;
                }
            case "/":
                {
                    return CalculatorOperation.Divide;
                }
            default:
                {
                    throw new InvalidOperationException("Wrong operation");
                }

        }
    }
}