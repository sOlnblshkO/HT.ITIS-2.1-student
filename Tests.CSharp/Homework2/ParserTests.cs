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
        // arrange
        var args = new[] { "10", operation, "20" };
        

        //act
        Parser.ParseCalcArguments(args, out var val1, out var operationResult, out var val2);

        //assert
        Assert.Equal(10, val1);
        Assert.Equal(operationExpected, operationResult);
        Assert.Equal(20, val2);
    }

    [HomeworkTheory(Homeworks.HomeWork2)]
    [InlineData("f", "+", "3")]
    [InlineData("3", "+", "f")]
    [InlineData("a", "+", "f")]
    public void TestParserWrongValues(string val1, string operation, string val2)
    {
        // arrange
        var args = new[] { val1, operation, val2 };

        //assert
        Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestParserWrongOperation()
    {
        // arrange
        var args = new[] { "20", "wrong", "4" };

        //assert
        Assert.Throws<InvalidOperationException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestParserWrongLength()
    {
        //arrange
        var args = new[] { "3", "-", "4", "5" };
        
        //assert
        Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
    }
}