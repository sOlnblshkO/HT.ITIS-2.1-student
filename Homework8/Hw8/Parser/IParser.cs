namespace Hw8.Parser;

public interface IParser
{
    bool ParseArgument(string input, out double val);
    Calculator.Operation ParseOperation(string operation);
}