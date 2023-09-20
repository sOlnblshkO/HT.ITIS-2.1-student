using Hw2;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework2;

public class ParserTests
{
    [HomeworkTheory(Homeworks.HomeWork2)]
    [InlineData("+", CalculatorOperation.Plus)]
    [InlineData("-", CalculatorOperation.Minus)]
    [InlineData("*", CalculatorOperation.Multiply)]
    [InlineData("/", CalculatorOperation.Divide)]
    public void TestCorrectOperations(string operation, CalculatorOperation operationExpected)
    {
        var args = new[] { "7", operation, "13" };
        
        Parser.ParseCalcArguments(args, out var val1, out var operationResult, out var val2);
        
        Assert.Equal(7, val1);
        Assert.Equal(operationResult, operationExpected);
        Assert.Equal(13, val2);
    }

    [HomeworkTheory(Homeworks.HomeWork2)]
    [InlineData("f", "+", "3")]
    [InlineData("3", "+", "f")]
    [InlineData("r", "+", "r")]
    public void TestParserWrongValues(string val1, string operation, string val2)
    {
        var args = new[] { val1, operation, val2 };

        Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestParserWrongOperation()
    {
        var args = new[] { "15", "nice", "16" };

        Assert.Throws<InvalidOperationException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestParserWrongLength()
    {
        var args = new[] { "14", "+", "16", "26" };

        Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }
}