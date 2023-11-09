using System.Globalization;
using Hw8.Calculator;

namespace Hw8.Services;

public class Calculator: ICalculator
{
    public double Plus(double val1, double val2)
    {
        return val1 + val2;
    }

    public double Minus(double val1, double val2)
    {
        return val1 - val2;
    }

    public double Multiply(double val1, double val2)
    {
        return val1 * val2;
    }

    public double Divide(double firstValue, double secondValue)
    {
        return secondValue == 0 ? throw new InvalidOperationException(Messages.DivisionByZeroMessage) : firstValue / secondValue;
    }

    public double Calculate(string val1, string oper, string val2)
    {
        var operation = ParseOperation(oper);
        var first = ParseArgument(val1);
        var second = ParseArgument(val2);
        var res = CalculateSwitchOperation(first, operation, second);
        return res;
    }

    private Operation ParseOperation(string operation)
    {
        switch (operation)
        {
            case "Plus": return Operation.Plus;
            case "Minus": return Operation.Minus;
            case "Multiply": return Operation.Multiply;
            case "Divide": return Operation.Divide;
            default: return Operation.Invalid;
        }
    }
    
    private static double ParseArgument(string arg)
    {
        if (double.TryParse(arg, NumberStyles.Float , CultureInfo.InvariantCulture, out var number))
        {
            return number;
        }

        throw new ArgumentException(Messages.InvalidNumberMessage);
    }

    private double CalculateSwitchOperation(double arg1, Operation operation, double arg2) =>
        operation switch
        {
            Operation.Plus => Plus(arg1, arg2),
            Operation.Minus => Minus(arg1, arg2),
            Operation.Multiply => Multiply(arg1, arg2),
            Operation.Divide => Divide(arg1, arg2),
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
}