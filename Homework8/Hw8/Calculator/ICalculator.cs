namespace Hw8.Calculator;

public interface ICalculator
{
    double Calculate(string arg1, string operation, string arg2);

    double Plus(double val1, double val2);
    
    double Minus(double val1, double val2);
    
    double Multiply(double val1, double val2);
    
    double Divide(double val1, double val2);
}