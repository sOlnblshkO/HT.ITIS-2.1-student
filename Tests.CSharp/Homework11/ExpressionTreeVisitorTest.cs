using System.Linq.Expressions;
using Hw11.Services.ExpressionUtils;
using Hw11.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework11;

public class ExpressionTreeVisitorTest
{
    [Homework(Homeworks.HomeWork11)]
    public void VisitBinaryInvalidOperation()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(3.0, typeof(double));
        ;
        
        var division = Expression.Power(constant1,constant2);
        

        Assert.Throws<Exception>( () => ExpressionTreeVisitorImpl.GetResult(division,1.0,1.0));
    }
}