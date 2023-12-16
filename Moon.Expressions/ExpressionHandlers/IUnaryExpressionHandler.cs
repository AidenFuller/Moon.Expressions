namespace Moon.Expressions.ExpressionHandlers;

public interface IUnaryExpressionHandler : IExpressionHandler
{ 
    SqlExpression Handle(SqlExpression expression);
}