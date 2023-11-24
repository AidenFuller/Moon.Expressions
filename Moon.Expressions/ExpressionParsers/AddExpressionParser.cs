using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.ExpressionParsers
{
    public class AddExpressionParser : IExpressionParser
    {
        private readonly IExpressionParserProvider _expressionParserProvider;

        public AddExpressionParser(IExpressionParserProvider expressionParserProvider)
        {
            _expressionParserProvider = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
        }

        public ExpressionType ExpressionType => ExpressionType.Add;

        public string Parse(Expression expression)
        {
            var andExpression = (BinaryExpression)expression;

            var left = _expressionParserProvider.GetParser(andExpression.Left).Parse(andExpression.Left);
            var right = _expressionParserProvider.GetParser(andExpression.Right).Parse(andExpression.Left);

            return $"{left} + {right}";
        }
    }
}
