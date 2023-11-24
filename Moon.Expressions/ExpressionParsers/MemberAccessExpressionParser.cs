using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class MemberAccessExpressionParser : IExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;
    private readonly IConstantResolver _constantResolver;

    public MemberAccessExpressionParser(IExpressionParserProvider expressionParserProvider, IConstantResolver constantResolver)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
    }

    public ExpressionType ExpressionType => ExpressionType.MemberAccess;

    public string Parse(Expression expression)
    {
        var memberExpression = (MemberExpression)expression;

        if (memberExpression.Expression.NodeType == ExpressionType.Constant)
        {
            var name = memberExpression.Member.Name;
            var value = ((ConstantExpression)memberExpression.Expression).Value;

            var field = value.GetType().GetField(name);
            var property = value.GetType().GetProperty(name);

            var rawValue = field != null ? field.GetValue(value) : property?.GetValue(value);

            return _constantResolver.Resolve(rawValue);
        }
        else
        {
            return _expressionParserProvider.GetParser(memberExpression.Expression!).Parse(memberExpression.Expression!);
        }
    }
}
