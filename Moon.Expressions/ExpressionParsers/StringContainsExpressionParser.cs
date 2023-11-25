using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers
{
    public class StringContainsExpressionParser : IExpressionParser
    {
        private readonly IExpressionParserFactory _expressionParserFactory;

        public StringContainsExpressionParser(IExpressionParserFactory expressionParserFactory)
        {
            _expressionParserFactory = expressionParserFactory ?? throw new ArgumentNullException(nameof(expressionParserFactory));
        }

        public string Parse(Expression expression)
        {
            var callExpression = (MethodCallExpression)expression;

            // Ignore further arguments, as the string comparison type is not relevant for SQL. Only the first expression matters
            var argument = callExpression.Arguments.First();

            var caller = _expressionParserFactory.ResolveAndParse(callExpression.Object);

            var argumentExpression = Expression.Add(Expression.Add(Expression.Constant("%"), argument, Methods.StringConcat), Expression.Constant("%"), Methods.StringConcat);
            var argumentValue = _expressionParserFactory.ResolveAndParse(argumentExpression);

            return $"{caller} LIKE {argumentValue}";
        }
    }
}
