using System.Runtime.InteropServices.ComTypes;

namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (!IsArgLengthSupported(args))
            throw new ArgumentException($"Wrong number of arguments \nExpected: 3  Found: {args.Length}");
        
        val1 = ParseNumber(args[0]);
        val2 = ParseNumber(args[2]);
        operation = ParseOperation(args[1]);
    }

    private static double ParseNumber(string number)
    {
        if (double.TryParse(number, out var result))
            return result;
        throw new ArgumentException($"Incorrect number: {number}");
    }

    private static bool IsArgLengthSupported(string[] args) => 
        args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg) =>
        arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => throw new InvalidOperationException($"Incorrect operation: {arg}")
        };
}