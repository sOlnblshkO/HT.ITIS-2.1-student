namespace Hw8.Calculator;

public interface ICalculatorParser<TUnparsedCalculatorOptions,TCalculatorOptions>
{
    public TCalculatorOptions ParseCalculatorArguments(TUnparsedCalculatorOptions options);
}