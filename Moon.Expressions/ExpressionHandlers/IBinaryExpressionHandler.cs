namespace Moon.Expressions.ExpressionHandlers;

public interface IBinaryExpressionHandler : IExpressionHandler
{
    BinaryExpressionType ExpressionType { get; }
    ISqlExpression Handle(ISqlExpression left, ISqlExpression right);
}