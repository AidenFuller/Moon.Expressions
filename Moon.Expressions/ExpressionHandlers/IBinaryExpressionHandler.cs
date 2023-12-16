namespace Moon.Expressions.ExpressionHandlers;

public interface IBinaryExpressionHandler : IExpressionHandler
{
    SqlExpression Handle(SqlExpression left, SqlExpression right);
}