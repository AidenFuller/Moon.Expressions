using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;
public class NullConstantExpressionHandler : IConstantExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.Null;

    public SqlExpression Handle(object? value)
    {
        if (value != null)
            throw new ArgumentException("Value is not null", nameof(value));

        return new SqlExpression { ExpressionType = FullExpressionType.Null, SqlString = "NULL" };
    }
}
