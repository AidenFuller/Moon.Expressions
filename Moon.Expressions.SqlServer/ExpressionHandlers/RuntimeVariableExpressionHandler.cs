using Moon.Expressions.ExpressionHandlers;
using System.Linq.Expressions;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;
public class RuntimeVariableExpressionHandler : IMemberExpressionHandler
{
    public FullExpressionType ExpressionType => FullExpressionType.RuntimeVariable;

    public SqlExpression Handle(string memberName, SqlExpression innerExpression)
    {
        if (innerExpression.ExpressionType != FullExpressionType.ConstantValue && innerExpression.ExpressionType != FullExpressionType.Null)
            throw new InvalidOperationException("A runtime variable must be evaluated to a constant or null expression");

        var keyValuePair = innerExpression.Parameters.Single();
        var value = keyValuePair.Value;
        var field = value.GetType().GetField(memberName);
        var property = value.GetType().GetProperty(memberName);

        var rawValue = field != null ? field.GetValue(value) : property?.GetValue(value);

        if (rawValue == null)
            return new SqlExpression { ExpressionType = FullExpressionType.Null, SqlString = "NULL" };
        else
            return new SqlExpression { ExpressionType = FullExpressionType.ConstantValue, SqlString = innerExpression.SqlString, Parameters = new() { [keyValuePair.Key] = rawValue } };
    }
}
