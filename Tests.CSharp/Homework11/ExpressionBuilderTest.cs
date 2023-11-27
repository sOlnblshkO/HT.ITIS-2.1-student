using System.Linq.Expressions;
using Hw11.Services.ExpressionUtils;
using Hw11.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework11;

public class ExpressionBuilderTest
{
    [Homework(Homeworks.HomeWork11)]
    public void VisitBinaryDivision()
    {
        Assert.Throws<Exception>(() => ExpressionBuilder.ConvertToExpression("1 3 ^"));
    }
}