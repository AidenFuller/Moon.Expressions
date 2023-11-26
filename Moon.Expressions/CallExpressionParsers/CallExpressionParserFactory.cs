using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Moon.Expressions.CallExpressionParsers;

public class CallExpressionParserFactory : IExpressionParserFactory
{
    private readonly Lazy<Dictionary<CallExpressionType, ICallExpressionParser>> _callExpressionParsersByType;
    private readonly ICallExpressionTypeEvaluator _callExpressionTypeEvaluator;

    public CallExpressionParserFactory(IServiceProvider serviceProvider, ICallExpressionTypeEvaluator callExpressionTypeEvaluator)
    {
        _callExpressionParsersByType = new Lazy<Dictionary<CallExpressionType, ICallExpressionParser>>(() => serviceProvider.GetServices<ICallExpressionParser>().ToDictionary(x => x.CallExpressionType));
        _callExpressionTypeEvaluator = callExpressionTypeEvaluator ?? throw new ArgumentNullException(nameof(callExpressionTypeEvaluator));
    }

    public ExpressionType ExpressionType => ExpressionType.Call;

    public IExpressionParser? ResolveParserFromExpression(Expression expression)
    {
        var callExpression = (MethodCallExpression)expression;
        var callExpressionType = _callExpressionTypeEvaluator.EvaluateExpressionType(callExpression);
        return _callExpressionParsersByType.Value.GetValueOrDefault(callExpressionType);
    }
}
