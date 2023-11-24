using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public class MemberAccessExpressionParser : IExpressionParser
{
    private readonly IExpressionParserFactory _expressionParserProvider;

    public MemberAccessExpressionParser(IExpressionParserFactory expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider;
    }

    public ExpressionType ExpressionType => ExpressionType.MemberAccess;

    public string Parse(Expression expression)
    {
        var memberExpression = (MemberExpression)expression;

        return _expressionParserProvider.GetParser(memberExpression.Expression!).Parse(memberExpression.Expression!);
    }
}
