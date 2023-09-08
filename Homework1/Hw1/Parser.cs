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
            throw new ArgumentException("Неправильное количество аргументов");
        }

        if (!(double.TryParse(args[0], out val1) && double.TryParse(args[2], out val2)))
        {
            throw new ArgumentException("Даны не числа");
        }

        operation = ParseOperation(args[1]);
        if (operation == CalculatorOperation.Undefined)
        {
            throw new InvalidOperationException("Дана неправильная операция");
        }
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
            _ => CalculatorOperation.Undefined
        };
    }
}