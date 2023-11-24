using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.ExpressionParsers
{
    public class ConstantExpressionParser : IExpressionParser
    {
        private readonly IConstantResolver _constantResolver;

        public ConstantExpressionParser(IConstantResolver constantResolver)
        {
            _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
        }

        public ExpressionType ExpressionType => ExpressionType.Constant;

        public string Parse(Expression expression)
        {
            var constantExpression = (ConstantExpression)expression;
            var value = constantExpression.Value;

            return _constantResolver.Resolve(value);
        }
    }
}
