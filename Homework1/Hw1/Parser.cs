namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (!IsArgLengthSupported(args)) 
            throw new ArgumentException("array length should be 3");

        if (ParseOperation(args[1]) == CalculatorOperation.Undefined)  
            throw new InvalidOperationException();
        
        
        if (Double.TryParse(args[0], out val1) && Double.TryParse(args[2], out val2))
        {
            operation = ParseOperation(args[1]);
        }
        else
            throw new ArgumentException("one of the numbers isn't actually a number");
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