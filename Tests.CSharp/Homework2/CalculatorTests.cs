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
        var resultValue = Calculator.Calculate(value1, operation, value2);
        
        Assert.Equal(expectedValue, resultValue);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestInvalidOperation()
    {
        Assert.Throws<InvalidOperationException>(() => Calculator.Calculate(2, CalculatorOperation.Undefined, 2));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingNonZeroByZero()
    {
        var resultValue = Calculator.Calculate(2, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.PositiveInfinity, resultValue);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByNonZero()
    {
        var resultValue = Calculator.Calculate(0, CalculatorOperation.Divide, 3);
        
        Assert.Equal(0, resultValue);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByZero()
    {
        var resultValue = Calculator.Calculate(0, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.NaN, resultValue);
    }
}