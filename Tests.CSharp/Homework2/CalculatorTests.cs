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
        var result = Calculator.Calculate(value1, operation, value2);
        
        Assert.Equal(expectedValue, result);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestInvalidOperation()
    {
        Assert.Throws<InvalidOperationException>(() => Hw2.Calculator.Calculate(1, CalculatorOperation.Undefined, 1));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingNonZeroByZero()
    {
        var result = Hw2.Calculator.Calculate(1, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.PositiveInfinity, result);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByNonZero()
    {
        var result = Hw2.Calculator.Calculate(0, CalculatorOperation.Divide, 1);
        
        Assert.Equal(0,result);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByZero()
    {
        var result = Hw2.Calculator.Calculate(0, CalculatorOperation.Divide, 0);
        
        Assert.Equal(double.NaN, result);
    }
}