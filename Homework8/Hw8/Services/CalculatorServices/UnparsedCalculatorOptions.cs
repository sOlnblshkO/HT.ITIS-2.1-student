namespace Hw8.Services.CalculatorServices;

public struct UnparsedCalculatorOptions
{
    public string Value1 { get; set; }
    public string Operation { get; set; }
    public string Value2 { get; set; }

    public UnparsedCalculatorOptions(string value1, string operation, string value2)
    {
        Value1 = value1;
        Operation = operation;
        Value2 = value2;
    }
}