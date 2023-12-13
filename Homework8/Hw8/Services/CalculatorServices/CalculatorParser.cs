using System.Globalization;
using Hw8.Calculator;

namespace Hw8.Services.CalculatorServices;

public class CalculatorParser : ICalculatorParser<UnparsedCalculatorOptions,CalculatorOptions>
{
    private static Dictionary<string, Operation> _calcOperationDictionary = new();

    static CalculatorParser()
    {
        _calcOperationDictionary["plus"] = Operation.Plus;
        _calcOperationDictionary["minus"] = Operation.Minus;
        _calcOperationDictionary["multiply"] = Operation.Multiply;
        _calcOperationDictionary["divide"] = Operation.Divide;
    }
        
    
    public CalculatorOptions ParseCalculatorArguments(UnparsedCalculatorOptions options)
    {
        if (!double.TryParse(options.Value1, NumberStyles.Any ,CultureInfo.InvariantCulture ,out var val1)
            || !double.TryParse(options.Value2, NumberStyles.Any ,CultureInfo.InvariantCulture ,out var val2))
            throw new InvalidDataException(Messages.InvalidNumberMessage);

        if (!TryParseCalculatorOperation(options.Operation.ToLower(), out var operation))
            throw new InvalidDataException(Messages.InvalidOperationMessage);

        return new CalculatorOptions(val1, operation, val2);
    }

    private bool TryParseCalculatorOperation(in string? stringOperation, out Operation operation)
    {
        if (stringOperation is null || !_calcOperationDictionary.ContainsKey(stringOperation))
        {
            operation = Operation.Invalid;  
            return false;
        }

        operation = _calcOperationDictionary[stringOperation];
        return true;
    }
}