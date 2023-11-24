using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class MemberAccessExpressionParser : IExpressionParser
{
    private readonly IExpressionParserFactory _expressionParserFactory;
    private readonly IConstantResolver _constantResolver;

    public MemberAccessExpressionParser(IExpressionParserFactory expressionParserFactory, IConstantResolver constantResolver)
    {
        _expressionParserFactory = expressionParserFactory ?? throw new ArgumentNullException(nameof(expressionParserFactory));
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
        else if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
        {
            var parameter = _expressionParserFactory.ResolveAndParse(memberExpression.Expression);
            return $"{parameter}.{memberExpression.Member.Name}";
        }
        else
        {
            return _expressionParserFactory.ResolveAndParse(memberExpression.Expression);
        }
    }
}
