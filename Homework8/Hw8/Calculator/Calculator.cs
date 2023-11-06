using System.Diagnostics.CodeAnalysis;

namespace Hw8.Calculator;

[ExcludeFromCodeCoverage]
public class Calculator : ICalculator
{
    public double Calculate(string arg1, string operation, string arg2)
    {
        Parser.ParseOperation(operation, out var parsedOperation);
        Parser.ParseArgs(arg1, arg2, out var val1, out var val2);

        return parsedOperation switch
        {
            Operation.Plus => Plus(val1, val2),
            Operation.Minus => Minus(val1, val2),
            Operation.Multiply => Multiply(val1, val2),
            Operation.Divide => Divide(val1, val2),
            _ => throw new InvalidOperationException(operation)
        };
    }

    public double Plus(double val1, double val2) => val1 + val2;
    
    public double Minus(double val1, double val2) => val1 - val2;
    
    public double Multiply(double val1, double val2) => val1 * val2;
    
    public double Divide(double val1, double val2) => val2 == 0 
        ? throw new DivideByZeroException(Messages.DivisionByZeroMessage)
        : val1 / val2;
}