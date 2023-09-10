namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        throw new NotImplementedException();
    }

    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        switch (arg)
        {
         case "+":
             return CalculatorOperation.Plus;
         case "-":
             return CalculatorOperation.Minus;
         case "*":
             return CalculatorOperation.Multiply;
         case "/":
             return CalculatorOperation.Divide;
         default:
             return CalculatorOperation.Undefined;
        }
        {
            
        }
        throw new NotImplementedException();
    }
}