using System.Linq.Expressions;
using Hw10.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework10;

public class ExpressionTreeVisitorTest
{
    [Homework(Homeworks.HomeWork10)]
    public void VisitBinaryDivision()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(2.0, typeof(double));
        
        var division = Expression.Divide(constant1, constant2);
        
        ExpressionVisitor expressionVisitor = new ExpressionTreeVisitorImpl();
        var result = expressionVisitor.Visit(division);
        
        var divide = Expression.Lambda<Func<double>>(result).Compile().Invoke();
        Assert.Equal(5.0,divide);
    }
    [Homework(Homeworks.HomeWork10)]
    public void VisitBinaryDivisionByZer0()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(0.0, typeof(double));
        
        var division = Expression.Divide(constant1, constant2);

        Assert.ThrowsAsync<Exception>(async () => await ExpressionTreeVisitorImpl.VisitBinary(division));
    }
    [Homework(Homeworks.HomeWork10)]
    public void VisitBinaryUnknownExpression()
    {
        var constant1 = Expression.Constant(5.0, typeof(double));
        
        var division = Expression.Decrement(constant1);
        

        Assert.ThrowsAsync<Exception>(async () => await ExpressionTreeVisitorImpl.VisitBinary(division));
    }
    [Homework(Homeworks.HomeWork10)]
    public void VisitBinaryInvalidOperation()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(3.0, typeof(double));
        ;
        
        var division = Expression.Power(constant1,constant2);
        

        Assert.ThrowsAsync<Exception>(async () => await ExpressionTreeVisitorImpl.VisitBinary(division));
    }
}