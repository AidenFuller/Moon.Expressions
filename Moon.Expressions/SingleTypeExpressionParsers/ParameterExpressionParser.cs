using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class ParameterExpressionParser : ISingleTypeExpressionParser
{
    public ExpressionType ExpressionType => ExpressionType.Parameter;

    public string Parse(Expression expression)
    {
        var parameterExpression = (ParameterExpression)expression;

        return parameterExpression.Name;
    }
}
