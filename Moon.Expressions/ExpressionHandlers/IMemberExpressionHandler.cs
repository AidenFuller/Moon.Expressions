using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionHandlers;

public interface IMemberExpressionHandler : IExpressionHandler
{
    SqlExpression Handle(string memberName, SqlExpression innerExpression);
}
