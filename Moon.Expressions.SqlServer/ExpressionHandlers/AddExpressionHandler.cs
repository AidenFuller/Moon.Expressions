using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;

public class AddExpressionHandler : IBinaryExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.Add;
    public SqlExpression Handle(SqlExpression left, SqlExpression right)
    {
        var leftString = left.GetBracketedSqlString(ExpressionType);
        var rightString = right.GetBracketedSqlString(ExpressionType);

        var sqlString = $"{leftString} + {rightString}";

        return new SqlExpression { ExpressionType = FullExpressionType.Add, SqlString = sqlString, ChildExpressions = new[] { left, right } };
    }
}
