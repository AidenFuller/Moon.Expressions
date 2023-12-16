using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;

public class ParameterMemberExpressionHandler : IMemberExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.ParameterMember;

    public SqlExpression Handle(string memberName, SqlExpression innerExpression)
    {
        if (innerExpression.ExpressionType != FullExpressionType.Parameter)
            throw new ArgumentException("Inner expression is not a parameter", nameof(innerExpression));

        var sqlString = $"{innerExpression.SqlString}.[{memberName}]";

        return new SqlExpression { ExpressionType = FullExpressionType.ParameterMember, SqlString = sqlString };
    }
}
