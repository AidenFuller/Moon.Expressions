using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.ExpressionHandlers;

public class ValueConstantExpressionHandler : IConstantExpressionHandler
{
    private readonly ExpressionParameterProvider _expressionParameterProvider;

    public FullExpressionType ExpressionType => FullExpressionType.ConstantValue;

    public ValueConstantExpressionHandler(ExpressionParameterProvider expressionParameterProvider)
    {
        _expressionParameterProvider = expressionParameterProvider;
    }

    public SqlExpression Handle(object? value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var parameterName = _expressionParameterProvider.GetUniqueParameterName("Value");

        return new SqlExpression { ExpressionType = FullExpressionType.ConstantValue, SqlString = $"@{parameterName}", Parameters = new Dictionary<string, object> { { parameterName, value } } };
    }
}
