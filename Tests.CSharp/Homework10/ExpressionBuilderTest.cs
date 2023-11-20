using System.Linq.Expressions;
using Hw9.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework10;

public class ExpressionBuilderTest
{
    [Homework(Homeworks.HomeWork9)]
    public void VisitBinaryDivision()
    {
        Assert.Throws<Exception>(() => ExpressionBuilder.ConvertToExpression("1 3 ^"));
    }
}