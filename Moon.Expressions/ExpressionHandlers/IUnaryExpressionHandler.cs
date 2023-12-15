namespace Moon.Expressions.ExpressionHandlers
{
    public interface IUnaryExpressionHandler
    { 
        UnaryExpressionType ExpressionType { get; }
        ISqlExpression Handle(ISqlExpression expression);
    }
}