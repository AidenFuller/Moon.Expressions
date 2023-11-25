using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.Extensions;

public static class IExpressionParserFactoryExtensions
{
    public static string ResolveAndParse(this IExpressionParserFactory expressionParserFactory, Expression expression)
    {
        return expressionParserFactory.GetParser(expression).Parse(expression);
    }
}
