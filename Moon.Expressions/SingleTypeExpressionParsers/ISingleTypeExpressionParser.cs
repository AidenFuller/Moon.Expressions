using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

/// <summary>
/// Expression Parser that performs the same operation for all expressions of a given Expression Type
/// </summary>
public interface ISingleTypeExpressionParser : IExpressionParser
{
    ExpressionType ExpressionType { get; }
}
