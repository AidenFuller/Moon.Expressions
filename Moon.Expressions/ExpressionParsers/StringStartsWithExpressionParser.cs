using Moon.Expressions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.ExpressionParsers
{
    public class StringStartsWithExpressionParser : IExpressionParser
    {
        private readonly IExpressionParserFactory _expressionParserFactory;

        public StringStartsWithExpressionParser(IExpressionParserFactory expressionParserFactory)
        {
            _expressionParserFactory = expressionParserFactory ?? throw new ArgumentNullException(nameof(expressionParserFactory));
        }

        public string Parse(Expression expression)
        {
            var callExpression = (MethodCallExpression)expression;

            // Ignore further arguments, as the string comparison type is not relevant for SQL. Only the first expression matters
            var argument = callExpression.Arguments.First();

            var caller = _expressionParserFactory.ResolveAndParse(callExpression.Object);

            var argumentExpression = Expression.Add(Expression.Constant("%"), argument, Methods.StringConcat);
            var argumentValue = _expressionParserFactory.ResolveAndParse(argumentExpression);

            return $"{caller} LIKE {argumentValue}";
        }
    }
}
