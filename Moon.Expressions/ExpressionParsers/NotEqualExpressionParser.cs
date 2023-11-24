using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class NotEqualExpressionParser : IExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public NotEqualExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public ExpressionType ExpressionType => ExpressionType.NotEqual;
    public string Parse(Expression expression)
    {
        var notEqualExpression = (BinaryExpression)expression;

        var left = _expressionParserProvider.GetParser(notEqualExpression.Left).Parse(notEqualExpression.Left);
        var right = _expressionParserProvider.GetParser(notEqualExpression.Right).Parse(notEqualExpression.Right);
        return $"{left} <> {right}";
    }
}
