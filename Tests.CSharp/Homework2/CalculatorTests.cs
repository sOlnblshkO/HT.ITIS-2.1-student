using Hw2;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework2;

public class CalculatorTests
{
    [HomeworkTheory(Homeworks.HomeWork2)]
    [InlineData(15, 5, CalculatorOperation.Plus, 20)]
    [InlineData(15, 5, CalculatorOperation.Minus, 10)]
    [InlineData(15, 5, CalculatorOperation.Multiply, 75)]
    [InlineData(15, 5, CalculatorOperation.Divide, 3)]
    public void TestAllOperations(int value1, int value2, CalculatorOperation operation, int expectedValue)
    {
        var operationResult = Calculator.Calculate(value1, operation, value2);
        
        Assert.Equal(expectedValue, operationResult);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestInvalidOperation()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Calculate(13, CalculatorOperation.Undefined, 24));    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingNonZeroByZero()
    {
        var result = Calculator.Calculate(10, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.PositiveInfinity, result);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByNonZero()
    {
        var result = Calculator.Calculate(0, CalculatorOperation.Divide, 10);
        
        Assert.Equal(0, result);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByZero()
    {
        var result = Calculator.Calculate(0, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.NaN, result);
    }
}