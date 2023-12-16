using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;
public class EqualExpressionHandler : IBinaryExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.Equal;

    public SqlExpression Handle(SqlExpression left, SqlExpression right)
    {
        var leftString = left.GetBracketedSqlString(ExpressionType);
        var rightString = right.GetBracketedSqlString(ExpressionType);

        var sqlString = $"{leftString} = {rightString}";

        return new SqlExpression { ExpressionType = FullExpressionType.Equal, SqlString = sqlString, ChildExpressions = new[] { left, right } };
    }
}
