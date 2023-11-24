using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.ExpressionParsers
{
    public class ParameterExpressionParser : IExpressionParser
    {
        public string Parse(Expression expression)
        {
            var parameterExpression = (ParameterExpression)expression;

            return parameterExpression.Name;
        }
    }
}
