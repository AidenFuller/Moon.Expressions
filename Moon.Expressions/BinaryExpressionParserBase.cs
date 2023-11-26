using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public abstract class BinaryExpressionParserBase : IExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserFactory;

    public BinaryExpressionParserBase(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserFactory = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public string Parse(Expression expression)
    {
        var binaryExpression = (BinaryExpression)expression;

        var left = _expressionParserFactory.ResolveAndParse(binaryExpression.Left);
        var right = _expressionParserFactory.ResolveAndParse(binaryExpression.Right);

        return Evaluate(left, right);
    }

    public abstract string Evaluate(string left, string right);
}