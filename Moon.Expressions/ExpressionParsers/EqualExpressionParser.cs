using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class EqualExpressionParser : IExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public EqualExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public ExpressionType ExpressionType => ExpressionType.Equal;
    public string Parse(Expression expression)
    {

        var equalExpression = (BinaryExpression)expression;

        var left = _expressionParserProvider.GetParser(equalExpression.Left).Parse(equalExpression.Left);
        var right = _expressionParserProvider.GetParser(equalExpression.Right).Parse(equalExpression.Right);
        return $"{left} = {right}";
    }
}
