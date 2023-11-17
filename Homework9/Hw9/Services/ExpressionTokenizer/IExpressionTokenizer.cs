namespace Hw9.Services.ExpressionTokenizer;

public interface IExpressionTokenizer
{
    public List<string> Tokenize(string expression);
}