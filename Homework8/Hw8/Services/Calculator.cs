using Hw8.Services;

namespace Hw8.Calculator;

public class Calculator : ICalculator   
{ 
    public double Plus(double val1, double val2) => val1 + val2;

    public double Minus(double val1, double val2) => val1 - val2;

    public double Multiply(double val1, double val2) => val1 * val2;

    public double Divide(double firstValue, double secondValue)
    {
        if (secondValue == 0)
            throw new DivideByZeroException(Messages.DivisionByZeroMessage); 
        return firstValue / secondValue;
    }
    
    public double Calculate(string val1, string op, string val2)
    {
        var (firstValue, operation , secondValue) = Parser.ParseArguments(val1, op, val2);

        return operation switch
        {
            Operation.Plus => Plus(firstValue, secondValue),
            Operation.Minus => Minus(firstValue, secondValue),
            Operation.Multiply => Multiply(firstValue, secondValue),
            Operation.Divide => Divide(firstValue, secondValue),
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }
}