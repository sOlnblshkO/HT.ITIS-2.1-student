namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (!IsArgLengthSupported(args))
            throw new ArgumentException("There must be three arguments!");
        
        val1 = ParseStringToDouble(args[0]);
        operation = ParseOperation(args[1]);
        val2 = ParseStringToDouble(args[2]);
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
            _ => throw new InvalidOperationException("No such operation exists")
        };
    }
    
    private static double ParseStringToDouble(string arg)
    {
        if (double.TryParse(arg, out var number))
            return number;
        throw new ArgumentException("Incorrect numeric entry");
    }   
}