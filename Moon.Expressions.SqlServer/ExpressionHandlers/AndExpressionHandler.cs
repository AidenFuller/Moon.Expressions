using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;

internal class AndExpressionHandler : IBinaryExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.And;

    public SqlExpression Handle(SqlExpression left, SqlExpression right)
    {
        var leftString = left.GetBracketedSqlString(ExpressionType);
        var rightString = right.GetBracketedSqlString(ExpressionType);
        var sqlString = $"{leftString} AND {rightString}";

        return new SqlExpression { ExpressionType = FullExpressionType.And, SqlString = sqlString, ChildExpressions = new[] { left, right } };
    }
}
