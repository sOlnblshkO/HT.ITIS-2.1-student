using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw11.ErrorMessages;

namespace Hw11.Services.MathCalculator.ExpressionBuilder;


[ExcludeFromCodeCoverage]
public class ExpressionTreeVisitor
{
   public static async Task<double> VisitExpression(Expression expression)
   {
       return await VisitAsync((dynamic)expression);
   }

   public static async Task<double> VisitAsync(ConstantExpression expression)
   {
       return (double)expression.Value!;
   }


    public static async Task<double> VisitAsync(BinaryExpression expression)
    {
        await Task.Delay(1000);
        var leftExpr = Task.Run(() => VisitExpression(expression.Left));
        var rightExpr = Task.Run(() => VisitExpression(expression.Right));
        var res = await Task.WhenAll(leftExpr, rightExpr);

        return Calculate(expression.NodeType, res[0], res[1]);
    }

    public static double Calculate(ExpressionType binExpr, double constLeft, double constRight)
    {
        return (binExpr) switch
        {
            ExpressionType.Add => constLeft + constRight,
            ExpressionType.Subtract => constLeft - constRight,
            ExpressionType.Multiply => constLeft * constRight,
            ExpressionType.Divide => constRight == 0.0
                ? throw new Exception(MathErrorMessager.DivisionByZero)
                : constLeft / constRight,
            _ => throw new Exception(MathErrorMessager.UnknownCharacter)
        };
    } 
}
