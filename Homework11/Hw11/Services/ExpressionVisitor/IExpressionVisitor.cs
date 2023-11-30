using System.Linq.Expressions;

namespace Hw11.Services.MyExpressionVisitor;

public interface IExpressionVisitor
{
    public Task<Expression> VisitExpression(Expression expression);
}