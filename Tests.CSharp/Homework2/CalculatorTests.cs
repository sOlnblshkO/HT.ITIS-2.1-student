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
        //act
        var actual = Calculator.Calculate(value1, operation, value2);

        //assert
        Assert.Equal(expectedValue, actual);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestInvalidOperation()
    {
        //arrange
        var value1 = 0;
        var value2 = 10;

        //assert
        Assert.Throws<InvalidOperationException>(() => Calculator.Calculate(value1, CalculatorOperation.Undefined, value2));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByNonZero()
    {
        //arrange
        var value1 = 0;
        var value2 = 10;

        //act
        var actual = Calculator.Calculate(value1, CalculatorOperation.Divide, value2);

        //assert
        Assert.Equal(0, actual);
    }

    [Homework(Homeworks.HomeWork2)] 
    public void TestDividingNonZeroByZero()
    {
        //arrange
        var value1 = 10;
        var value2 = 0;

        //act
        var actual = Calculator.Calculate(value1, CalculatorOperation.Divide, value2);

        //assert
        Assert.Equal(double.PositiveInfinity, actual);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByZero()
    {
        //arrange
        var value1 = 0;
        var value2 = 0;

        //act
        var actual = Calculator.Calculate(value1, CalculatorOperation.Divide, value2);

        //assert
        Assert.Equal(double.NaN, actual);
    }
}