using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.SingleTypeExpressionParsers;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserProvider
{
    IExpressionParser GetParser(Expression expression);
}

public class ExpressionParserProvider : IExpressionParserProvider
{
    private readonly IConstantResolver _constantResolver;
    private readonly Lazy<Dictionary<ExpressionType, ISingleTypeExpressionParser>> _singleTypeExpressionParsersByExpressionType;
    private readonly Lazy<Dictionary<ExpressionType, IExpressionParserFactory>> _expressionParserFactoriesByExpressionType;

    public ExpressionParserProvider(IConstantResolver constantResolver, IServiceProvider serviceProvider)
    {
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
        _singleTypeExpressionParsersByExpressionType = new Lazy<Dictionary<ExpressionType, ISingleTypeExpressionParser>>(() => serviceProvider.GetServices<ISingleTypeExpressionParser>().ToDictionary(x => x.ExpressionType));
        _expressionParserFactoriesByExpressionType = new Lazy<Dictionary<ExpressionType, IExpressionParserFactory>>(() => serviceProvider.GetServices<IExpressionParserFactory>().ToDictionary(x => x.ExpressionType));
    }

    public IExpressionParser GetParser(Expression expression)
    {
        if (_singleTypeExpressionParsersByExpressionType.Value.TryGetValue(expression.NodeType, out var singleTypeParser))
        {
            return singleTypeParser;
        }
        else if (_expressionParserFactoriesByExpressionType.Value.TryGetValue(expression.NodeType, out var factory))
        {
            var parser = factory.ResolveParserFromExpression(expression);
            if (parser != null)
                return parser;
        }

        throw new InvalidOperationException($"Expression parser or factory for {expression.NodeType} not found.");
    }
}