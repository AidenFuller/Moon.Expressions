namespace Moon.Expressions;

public class SqlExpression
{
    public FullExpressionType ExpressionType { get; set; }
    public SqlExpression[] ChildExpressions { get; set; } = Array.Empty<SqlExpression>();
    public string SqlString { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}

public static class SqlExpressionExtensions
{
    public static string GetBracketedSqlString(this SqlExpression expression, FullExpressionType parentExpressionType)
    {
        return expression.ExpressionType.ShouldWrapInBrackets(parentExpressionType)
            ? $"({expression.SqlString})"
            : expression.SqlString;
    }

    public static Dictionary<string, object> GetAllParameters(this SqlExpression expression)
    {
        return expression.ChildExpressions
            .SelectMany(x => x.GetAllParameters())
            .Concat(expression.Parameters)
            .ToDictionary(x => x.Key, x => x.Value);
    }
}