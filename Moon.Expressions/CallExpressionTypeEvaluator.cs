using System.Collections;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface ICallExpressionTypeEvaluator
{
    CallExpressionType EvaluateExpressionType(MethodCallExpression expression);
}

public enum CallExpressionType
{
    StringContains,
    StringStartsWith,
    StringEndsWith,
    EnumerableContains,
}

internal class CallExpressionTypeEvaluator : ICallExpressionTypeEvaluator
{
    public CallExpressionType EvaluateExpressionType(MethodCallExpression expression)
    {
        var callerType = expression.Object?.Type;
        var calledMethod = expression.Method.Name;
        var isExtensionMethod = callerType == null;

        if (callerType == typeof(string) && calledMethod == nameof(string.Contains)) return CallExpressionType.StringContains;
        if (callerType == typeof(string) && calledMethod == nameof(string.StartsWith)) return CallExpressionType.StringStartsWith;
        if (callerType == typeof(string) && calledMethod == nameof(string.EndsWith)) return CallExpressionType.StringEndsWith;

        if (IsEnumerableExtension(expression) && calledMethod == nameof(Enumerable.Contains)) return CallExpressionType.EnumerableContains;

        throw new NotSupportedException($"Call Expression with caller type {callerType?.Name} and method {calledMethod} is not supported.");
    }

    public bool IsEnumerableExtension(MethodCallExpression expression) => expression.Object == null && expression.Arguments.Any() && typeof(IEnumerable).IsAssignableFrom(expression.Arguments[0].Type);
}
