using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;
public class ParameterExpressionHandler : IParameterExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.Parameter;

    public SqlExpression Handle(string parameterName)
    {
        return new SqlExpression { ExpressionType = FullExpressionType.Parameter, SqlString = $"[{parameterName}]" };
    }
}
