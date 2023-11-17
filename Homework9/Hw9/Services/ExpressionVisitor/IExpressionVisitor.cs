using System.Linq.Expressions;

namespace Hw9.Services.MyExpressionVisitor;

public interface IExpressionVisitor
{
    public Task<Expression> VisitExpression(Expression expression);
}