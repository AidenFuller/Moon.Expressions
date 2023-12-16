using System.Linq.Expressions;

namespace Moon.Expressions.Extensions;

public static class IExpressionParserFactoryExtensions
{
    public static string ResolveAndParse(this IExpressionParserProvider expressionParserFactory, Expression expression)
    {
        return expressionParserFactory.GetParser(expression).Parse(expression);
    }
}
