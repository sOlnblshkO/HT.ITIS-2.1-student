namespace Hw10.Services.MathCalculator.ExpressionTokenizer;

public interface IExpressionTokenizer
{
    public List<string> Tokenize(string expression);
}