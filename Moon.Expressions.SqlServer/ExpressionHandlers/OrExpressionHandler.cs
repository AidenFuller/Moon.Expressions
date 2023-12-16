using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;
public class OrExpressionHandler : IBinaryExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.Or;

    public SqlExpression Handle(SqlExpression left, SqlExpression right)
    {
        var leftString = left.GetBracketedSqlString(ExpressionType);
        var rightString = right.GetBracketedSqlString(ExpressionType);
        var sqlString = $"{leftString} OR {rightString}";

        return new SqlExpression { ExpressionType = FullExpressionType.Or, SqlString = sqlString, ChildExpressions = new[] { left, right } };
    }
}
