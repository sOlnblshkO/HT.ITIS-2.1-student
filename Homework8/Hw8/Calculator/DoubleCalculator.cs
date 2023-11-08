namespace Hw8.Calculator;

public class DoubleCalculator : ICalculator
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

    public double Divide(double val1, double val2)
    {
        if (val2 == 0)
            throw new InvalidOperationException();
        return val1 / val2;
    }
}