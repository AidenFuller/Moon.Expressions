using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class OrExpressionParser : IExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public OrExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public ExpressionType ExpressionType => ExpressionType.OrElse;

    public string Parse(Expression expression)
    {
        var orExpression = (BinaryExpression)expression;

        var left = _expressionParserProvider.GetParser(orExpression.Left).Parse(orExpression.Left);
        var right = _expressionParserProvider.GetParser(orExpression.Right).Parse(orExpression.Right);

        return $"({left}) OR ({right})";
    }
}
