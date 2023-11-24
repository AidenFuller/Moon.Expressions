using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParser
{
    string Parse(Expression expression);
}