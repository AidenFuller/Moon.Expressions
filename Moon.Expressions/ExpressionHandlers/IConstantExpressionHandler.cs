namespace Moon.Expressions.ExpressionHandlers;

public interface IConstantExpressionHandler : IExpressionHandler
{ 
    SqlExpression Handle(object? value);
}