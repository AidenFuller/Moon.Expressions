using Moon.Expressions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.CallExpressionParsers;

public class DateBetweenExpressionParser : ICallExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public DateBetweenExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public CallExpressionType CallExpressionType => CallExpressionType.DateBetween;

    public string Parse(Expression expression)
    {
        var methodCallExpression = (MethodCallExpression)expression;

        var dateExpression = methodCallExpression.Arguments[0];
        var startExpression = methodCallExpression.Arguments[1];
        var endExpression = methodCallExpression.Arguments[2];

        var date = _expressionParserProvider.ResolveAndParse(dateExpression);
        var start = _expressionParserProvider.ResolveAndParse(startExpression);
        var end = _expressionParserProvider.ResolveAndParse(endExpression);

        return $"{date} BETWEEN {start} AND {end}";
    }
}
