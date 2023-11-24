using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParser
{
    ExpressionType ExpressionType { get; }
    string Parse(Expression expression);
}