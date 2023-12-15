using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class NewInstanceExpressionParser : ISingleTypeExpressionParser
{
    private readonly HashSet<Type> _supportedTypes = new() { typeof(DateOnly), typeof(DateTime), typeof(DateTimeOffset) };
    public ExpressionType ExpressionType => ExpressionType.New;

    public string Parse(Expression expression)
    {
        var newInstanceExpression = (NewExpression)expression;
        
        
        return null;
    }
}
