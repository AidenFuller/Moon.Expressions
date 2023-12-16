using Moon.Expressions.Extensions;

namespace Moon.Expressions.ExpressionHandlers;

public interface IExpressionHandler
{ 
    FullExpressionType ExpressionType { get; }
}