namespace Hw1;

public static class Parser
{
    /// <summary>
    /// Извлекает оператор и операнды из массива строк
    /// </summary>
    /// <param name="args"></param>
    /// <param name="val1"></param>
    /// <param name="operation"></param>
    /// <param name="val2"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out CalculatorOperation operation,
        out double val2)
    {
        //Проверка валидности количества аргументов
        if (!IsArgLengthSupported(args))
        {
            throw new ArgumentException("Invalid argument count");
        }

        if (!double.TryParse(args[0], out val1))
        {
            //Первый аргумент не валидный
            throw new ArgumentException("First argument invalid");
        }
        if (!double.TryParse(args[2], out val2))
        {
            //Второй аргумент не валидный
            throw new ArgumentException("Second argument invalid");
        }
        operation = ParseOperation(args[1]);
    }

    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    /// <summary>
    /// Преобразует операцию из строки в CalculatorOperation
    /// </summary>
    /// <param name="arg">Операция в виде строки</param>
    /// <returns>Операция в виде CalculatorOperation</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => throw new InvalidOperationException($"Invalid operation: {arg}")
        };
    }
}