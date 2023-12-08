using Hw8.Calculator;

namespace Hw8.Services.CalculatorServices;

public struct CalculatorOptions
{
    public double Value1 { get; set; }
    public Operation Operation { get; set; }
    public double Value2 { get; set; }

    public CalculatorOptions(double value1, Operation operation, double value2)
    {
        Value1 = value1;
        Operation = operation;
        Value2 = value2;
    }
}