using System.Linq.Expressions;
using Hw9.Services.MathCalculator;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework9;

public class ExpressionTreeVisitorTest
{
    [Homework(Homeworks.HomeWork9)]
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
    [Homework(Homeworks.HomeWork9)]
    public void VisitBinaryDivisionByZer0()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(0.0, typeof(double));
        
        var division = Expression.Divide(constant1, constant2);
        
        ExpressionVisitor expressionVisitor = new ExpressionTreeVisitorImpl();

        Assert.Throws<Exception>(() => expressionVisitor.Visit(division));
    }
    [Homework(Homeworks.HomeWork9)]
    public void VisitBinaryUnknownExpression()
    {
        var constant1 = Expression.Constant(10.0, typeof(double));
        var constant2 = Expression.Constant(3.0, typeof(double));
        
        var division = Expression.Power(constant1, constant2);
        
        ExpressionVisitor expressionVisitor = new ExpressionTreeVisitorImpl();

        Assert.Throws<Exception>(() => expressionVisitor.Visit(division));
    }
}