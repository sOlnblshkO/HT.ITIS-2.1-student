using System.Linq.Expressions;

namespace Hw11.Services.MyExpressionVisitor;

public interface IExpressionVisitor
{
    public Expression VisitExpression(Expression expression);
}