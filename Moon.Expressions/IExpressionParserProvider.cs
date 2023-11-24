using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserProvider
{
    IExpressionParser GetParser(Expression expression);
}

public class ExpressionParserProvider : IExpressionParserProvider
{
    private readonly Lazy<Dictionary<ExpressionType, IExpressionParser>> _expressionParsersByType;

    public ExpressionParserProvider(IServiceProvider serviceProvider)
    {
        _expressionParsersByType = new Lazy<Dictionary<ExpressionType, IExpressionParser>>(() => serviceProvider.GetServices<IExpressionParser>().ToDictionary(x => x.ExpressionType));
    }

    public IExpressionParser GetParser(Expression expression)
    {
        if (_expressionParsersByType.Value.TryGetValue(expression.NodeType, out var parser))
        {
            return parser;
        }
        throw new NotSupportedException($"Cannot find an expression parser for Node Type {expression.NodeType}");
    }
}