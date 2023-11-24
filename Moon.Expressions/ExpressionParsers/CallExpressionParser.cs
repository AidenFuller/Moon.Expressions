using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.ExpressionParsers
{
    public class CallExpressionParser : IExpressionParser
    {
        public string Parse(Expression expression)
        {
            var callExpression = (MethodCallExpression)expression;
            return null;
        }
    }
}
