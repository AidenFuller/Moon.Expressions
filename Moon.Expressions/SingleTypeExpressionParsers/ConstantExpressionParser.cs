using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class ConstantExpressionParser : ISingleTypeExpressionParser
{
    private readonly IConstantResolver _constantResolver;

    public ConstantExpressionParser(IConstantResolver constantResolver)
    {
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
    }

    public ExpressionType ExpressionType => ExpressionType.Constant;

    public string Parse(Expression expression)
    {
        var constantExpression = (ConstantExpression)expression;
        var value = constantExpression.Value;

        return _constantResolver.Resolve(value);
    }
}
