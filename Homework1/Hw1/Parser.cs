namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (IsArgLengthSupported(args))
        {
            if (double.TryParse(args[0], out val1))
            {
                Convert.ToDouble(args[0]);
            }
            else
            {
                throw new ArgumentException();
            }

            operation = ParseOperation(args[1]);

            if (double.TryParse(args[2], out val2))
            {
                Convert.ToDouble(args[2]);
            }
            else
            {
                throw new ArgumentException();
            }

            if (!(double.TryParse(args[0], out val1) && double.TryParse(args[2], out val1)))
            {
                throw new ArgumentException();
            }
        }
        else
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
            "-" =>  CalculatorOperation.Minus,
            "*" =>  CalculatorOperation.Multiply,
            "/" =>  CalculatorOperation.Divide,
            _ => throw new InvalidOperationException()
        };
    }
}