using System.Linq.Expressions;
using Moon.Expressions.Extensions;

namespace Moon.Expressions;

public interface IExpressionTypeCalculator
{
    BinaryExpressionType Calculate(BinaryExpression expression);
    UnaryExpressionType Calculate(UnaryExpression expression);
    MethodCallExpressionType Calculate(MethodCallExpression expression);
    ConstantExpressionType Calculate(ConstantExpression expression);
}

public class ExpressionTypeCalculator : IExpressionTypeCalculator
{
    public BinaryExpressionType Calculate(BinaryExpression expression) => expression.NodeType switch
    {
        ExpressionType.AndAlso => BinaryExpressionType.And,
        ExpressionType.OrElse => BinaryExpressionType.Or,
        ExpressionType.Equal => BinaryExpressionType.Equal,
        ExpressionType.NotEqual => BinaryExpressionType.NotEqual,
        ExpressionType.GreaterThan => BinaryExpressionType.GreaterThan,
        ExpressionType.GreaterThanOrEqual => BinaryExpressionType.GreaterThanOrEqual,
        ExpressionType.LessThan => BinaryExpressionType.LessThan,
        ExpressionType.LessThanOrEqual => BinaryExpressionType.LessThanOrEqual,
        ExpressionType.Add => BinaryExpressionType.Add,
        ExpressionType.Subtract => BinaryExpressionType.Subtract,
        ExpressionType.Multiply => BinaryExpressionType.Multiply,
        ExpressionType.Divide => BinaryExpressionType.Divide,
        ExpressionType.Coalesce => BinaryExpressionType.Coalesce,
        ExpressionType.Conditional => BinaryExpressionType.Conditional,
        _ => throw new NotSupportedException($"Binary expression type {expression.NodeType} is not supported.")
    };

    public UnaryExpressionType Calculate(UnaryExpression expression) => expression.NodeType switch
    {
        ExpressionType.Not => UnaryExpressionType.Not,
        ExpressionType.Convert => UnaryExpressionType.Cast,
        _ => throw new NotSupportedException($"Unary expression type {expression.NodeType} is not supported.")
    };

    public ConstantExpressionType Calculate(ConstantExpression expression) => expression.Value switch
    {
        null => ConstantExpressionType.Null,
        _ => ConstantExpressionType.Value
    };

    public MethodCallExpressionType Calculate(MethodCallExpression expression)
    {
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.Contains)) return MethodCallExpressionType.Contains;
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.StartsWith)) return MethodCallExpressionType.StartsWith;
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.EndsWith)) return MethodCallExpressionType.EndsWith;
        if (expression.Method.DeclaringType == typeof(ExpressionMethodExtensions) && expression.Method.Name == nameof(ExpressionMethodExtensions.In)) return MethodCallExpressionType.In;
        if (expression.Method.DeclaringType == typeof(ExpressionMethodExtensions) && expression.Method.Name == nameof(ExpressionMethodExtensions.Between)) return MethodCallExpressionType.Between;
        throw new NotSupportedException($"Method call expression type {expression.Method.Name} is not supported.");
    }
}