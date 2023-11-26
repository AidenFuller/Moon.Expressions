using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserFactory
{
    ExpressionType ExpressionType { get; }
    IExpressionParser? ResolveParserFromExpression(Expression expression);
}
