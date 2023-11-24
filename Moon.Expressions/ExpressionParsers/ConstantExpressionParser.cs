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
        public ExpressionType ExpressionType => ExpressionType.Constant;

        public string Parse(Expression expression)
        {
            var constantExpression = (ConstantExpression)expression;
            var value = constantExpression.Value;

            if (value == null)
            {
                return "NULL";
            }
            else if (value is char c)
            {
                return $"'{c}'";
            }
            else if (value is string text)
            {
                return $"'{text}'";
            }
            else if (value is bool bit)
            {
                return bit ? "1" : "0";
            }
            else if (value is int i)
            {
                return i.ToString();
            }
            else if (value is decimal dec)
            {
                return $"{dec:D}";
            }
            else if (value is double dbl)
            {
                return dbl.ToString();
            }
            else if (value is DateTime dt)
            {
                return $"'{dt:yyyy-MM-dd hh:mm:ss}'";
            }
            else if (value is DateOnly dateOnly)
            {
                return $"'{dateOnly:yyyy-MM-dd}'";
            }
            else
            {
                throw new NotSupportedException("The type of this constant is not supported");
            }
        }
    }
}
