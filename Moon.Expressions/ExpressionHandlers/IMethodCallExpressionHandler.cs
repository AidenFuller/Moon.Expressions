namespace Moon.Expressions.ExpressionHandlers;

public interface IMethodCallExpressionHandler
{
    MethodCallExpressionType ExpressionType { get; }
    ISqlExpression Handle(ISqlExpression? caller, params ISqlExpression[] arguments);
}