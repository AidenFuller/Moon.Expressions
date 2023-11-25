using Moon.Expressions.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Moon.Expressions.ExpressionParsers;

public class StringEndsWithExpressionParser : IExpressionParser
{
    private readonly IExpressionParserFactory _expressionParserFactory;

    public StringEndsWithExpressionParser(IExpressionParserFactory expressionParserFactory)
    {
        _expressionParserFactory = expressionParserFactory ?? throw new ArgumentNullException(nameof(expressionParserFactory));
    }

    public string Parse(Expression expression)
    {
        var callExpression = (MethodCallExpression)expression;

        // Ignore further arguments, as the string comparison type is not relevant for SQL. Only the first expression matters
        var argument = callExpression.Arguments.First();

        var caller = _expressionParserFactory.ResolveAndParse(callExpression.Object);

        var argumentExpression = Expression.Add(argument, Expression.Constant("%"), Methods.StringConcat);
        var argumentValue = _expressionParserFactory.ResolveAndParse(argumentExpression);

        return $"{caller} LIKE {argumentValue}";
    }
}
