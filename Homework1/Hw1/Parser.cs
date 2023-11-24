namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(IReadOnlyCollection<string> args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (!IsArgLengthSupported(args))
            throw new ArgumentException($"Need 3 arguments, was given {args.Count}");

        if (!(double.TryParse(args.ElementAt(0), out val1) && double.TryParse(args.ElementAt(2), out val2)))
            throw new ArgumentException($"Wrong arguments, expected numbers, was given {args.ElementAt(0)} and {args.ElementAt(2)}");

        operation = ParseOperation(args.ElementAt(1));
    }

    private static bool IsArgLengthSupported(IReadOnlyCollection<string> args) => args.Count == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => throw new InvalidOperationException($"Expected operation(+, -, *, /), was given {arg}")
        };
    }
}