using Moon.Expressions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.CallExpressionParsers;

public class EnumerableContainsExpressionParser : ICallExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public EnumerableContainsExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public CallExpressionType CallExpressionType => CallExpressionType.EnumerableContains;

    public string Parse(Expression expression)
    {
        var methodCallExpression = (MethodCallExpression)expression;

        var enumerableExpression = methodCallExpression.Arguments[0];
        var subjectExpression = methodCallExpression.Arguments[1];

        var enumerable = _expressionParserProvider.ResolveAndParse(enumerableExpression);
        var subject = _expressionParserProvider.ResolveAndParse(subjectExpression);

        return $"{subject} IN {enumerable}";
    }
}
