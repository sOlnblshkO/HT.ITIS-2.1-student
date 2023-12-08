using System.Globalization;
using Hw8.Calculator;

namespace Hw8.Services.CalculatorServices;

public class CalculatorParserService : ICalculatorParser<CalculatorOptions>
{
    private static Dictionary<string, Operation> _calcOperationDictionary = new();

    static CalculatorParserService()
    {
        _calcOperationDictionary["Plus"] = Operation.Plus;
        _calcOperationDictionary["Minus"] = Operation.Minus;
        _calcOperationDictionary["Multiply"] = Operation.Multiply;
        _calcOperationDictionary["Divide"] = Operation.Divide;
    }
        
    
    public CalculatorOptions ParseCalculatorArguments(IReadOnlyCollection<string> args)
    {
        var stringValue1 = args.ElementAt(0);
        var stringOperation = args.ElementAt(1);
        var stringValue2 = args.ElementAt(2);

        if (!double.TryParse(stringValue1, NumberStyles.Any ,CultureInfo.InvariantCulture ,out var val1)
            || !double.TryParse(stringValue2, NumberStyles.Any ,CultureInfo.InvariantCulture ,out var val2))
            throw new InvalidDataException(Messages.InvalidNumberMessage);

        if (!TryParseCalculatorOperation(stringOperation, out var operation))
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