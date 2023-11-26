using Moon.Expressions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class NewArrayInitExpressionParser : ISingleTypeExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserProvider;

    public NewArrayInitExpressionParser(IExpressionParserProvider expressionParserProvider)
    {
        _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public ExpressionType ExpressionType => ExpressionType.NewArrayInit;

    public string Parse(Expression expression)
    {
        var arrayExpression = (NewArrayExpression)expression;

        return $"({string.Join(", ", arrayExpression.Expressions.Select(x => _expressionParserProvider.ResolveAndParse(x)))})";
    }
}
