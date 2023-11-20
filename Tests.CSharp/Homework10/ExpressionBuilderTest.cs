using System.Linq.Expressions;
using Hw10.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework10;

public class ExpressionBuilderTest
{
    [Homework(Homeworks.HomeWork10)]
    public void VisitBinaryDivision()
    {
        Assert.Throws<Exception>(() => ExpressionBuilder.ConvertToExpression("1 3 ^"));
    }
}