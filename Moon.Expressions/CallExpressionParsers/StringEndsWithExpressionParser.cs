using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.CallExpressionParsers;

public class StringEndsWithExpressionParser : ICallExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserFactory;

    public StringEndsWithExpressionParser(IExpressionParserProvider expressionParserFactory)
    {
        _expressionParserFactory = expressionParserFactory ?? throw new ArgumentNullException(nameof(expressionParserFactory));
    }

    public CallExpressionType CallExpressionType => CallExpressionType.StringEndsWith;

    public string Parse(Expression expression)
    {
        var methodCallExpression = (MethodCallExpression)expression;

        // Ignore further arguments, as the string comparison type is not relevant for SQL. Only the first expression matters
        var argument = methodCallExpression.Arguments.First();

        var caller = _expressionParserFactory.ResolveAndParse(methodCallExpression.Object);

        var argumentExpression = Expression.Add(argument, Expression.Constant("%"), Methods.StringConcat);
        var argumentValue = _expressionParserFactory.ResolveAndParse(argumentExpression);

        return $"{caller} LIKE {argumentValue}";
    }
}
