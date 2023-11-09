using System.Globalization;
using Hw8.Calculator;

namespace Hw8.Parser;

public class DoubleParser : IParser
{
    public string Parse(string val1, string operation, string val2, ICalculator calculator)
    {
        if (!(double.TryParse(val1, out var doubleVal1) && double.TryParse(val2, out var doubleVal2)))
            return Messages.InvalidNumberMessage;
        if (!Enum.TryParse<Operation>(operation, true, out var parsedOperation))
            return Messages.InvalidOperationMessage;
        if (parsedOperation == Operation.Divide && doubleVal2 == 0)
            return Messages.DivisionByZeroMessage;
        return parsedOperation switch
        {
            Operation.Plus => calculator.Plus(doubleVal1, doubleVal2).ToString(CultureInfo.InvariantCulture),
            Operation.Minus => calculator.Minus(doubleVal1, doubleVal2).ToString(CultureInfo.InvariantCulture),
            Operation.Multiply => calculator.Multiply(doubleVal1, doubleVal2).ToString(CultureInfo.InvariantCulture),
            Operation.Divide => calculator.Divide(doubleVal1, doubleVal2).ToString(CultureInfo.InvariantCulture),
            _ => Messages.InvalidOperationMessage
        };
    }
}