namespace Hw8.Calculator;

public interface ICalculatorParser<out TCalculatorOptions>
{
    public TCalculatorOptions ParseCalculatorArguments(IReadOnlyCollection<string> args);
}