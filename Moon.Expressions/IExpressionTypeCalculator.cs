using System.Linq.Expressions;
using Moon.Expressions.Extensions;

namespace Moon.Expressions;

public interface IExpressionTypeCalculator
{
    FullExpressionType Calculate(BinaryExpression expression);
    FullExpressionType Calculate(UnaryExpression expression);
    FullExpressionType Calculate(MethodCallExpression expression);
    FullExpressionType Calculate(ConstantExpression expression);
    FullExpressionType Calculate(MemberExpression expression);
    FullExpressionType Calculate(ParameterExpression expression);
}

public class ExpressionTypeCalculator : IExpressionTypeCalculator
{
    public FullExpressionType Calculate(BinaryExpression expression) => expression.NodeType switch
    {
        ExpressionType.AndAlso => FullExpressionType.And,
        ExpressionType.OrElse => FullExpressionType.Or,
        ExpressionType.Equal => FullExpressionType.Equal,
        ExpressionType.NotEqual => FullExpressionType.NotEqual,
        ExpressionType.GreaterThan => FullExpressionType.GreaterThan,
        ExpressionType.GreaterThanOrEqual => FullExpressionType.GreaterThanOrEqual,
        ExpressionType.LessThan => FullExpressionType.LessThan,
        ExpressionType.LessThanOrEqual => FullExpressionType.LessThanOrEqual,
        ExpressionType.Add => FullExpressionType.Add,
        ExpressionType.Subtract => FullExpressionType.Subtract,
        ExpressionType.Multiply => FullExpressionType.Multiply,
        ExpressionType.Divide => FullExpressionType.Divide,
        ExpressionType.Coalesce => FullExpressionType.Coalesce,
        ExpressionType.Conditional => FullExpressionType.Conditional,
        _ => throw new NotSupportedException($"Binary expression type {expression.NodeType} is not supported.")
    };

    public FullExpressionType Calculate(UnaryExpression expression) => expression.NodeType switch
    {
        ExpressionType.Not => FullExpressionType.Not,
        ExpressionType.Convert => FullExpressionType.Cast,
        _ => throw new NotSupportedException($"Unary expression type {expression.NodeType} is not supported.")
    };

    public FullExpressionType Calculate(ConstantExpression expression) => expression.Value switch
    {
        null => FullExpressionType.Null,
        _ => FullExpressionType.ConstantValue
    };

    public FullExpressionType Calculate(MethodCallExpression expression)
    {
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.Contains)) return FullExpressionType.Contains;
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.StartsWith)) return FullExpressionType.StartsWith;
        if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.EndsWith)) return FullExpressionType.EndsWith;
        if (expression.Method.DeclaringType == typeof(ExpressionMethodExtensions) && expression.Method.Name == nameof(ExpressionMethodExtensions.In)) return FullExpressionType.In;
        if (expression.Method.DeclaringType == typeof(ExpressionMethodExtensions) && expression.Method.Name == nameof(ExpressionMethodExtensions.Between)) return FullExpressionType.Between;
        throw new NotSupportedException($"Method call expression type {expression.Method.Name} is not supported.");
    }

    public FullExpressionType Calculate(MemberExpression expression) => expression.Expression.NodeType switch
    {
        ExpressionType.Constant => FullExpressionType.RuntimeVariable,
        ExpressionType.Parameter => FullExpressionType.ParameterMember,
        _ => throw new NotSupportedException($"Member expression type {expression.Expression.NodeType} is not supported.")
    };

    public FullExpressionType Calculate(ParameterExpression expression) => expression.NodeType switch
    {
        ExpressionType.Parameter => FullExpressionType.Parameter,
        _ => throw new NotSupportedException($"Parameter expression type {expression.NodeType} is not supported.")
    };
}