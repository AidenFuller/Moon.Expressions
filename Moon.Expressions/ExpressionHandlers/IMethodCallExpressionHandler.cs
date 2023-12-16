namespace Moon.Expressions.ExpressionHandlers;

public interface IMethodCallExpressionHandler : IExpressionHandler
{
    SqlExpression Handle(SqlExpression? caller, params SqlExpression[] arguments);
}