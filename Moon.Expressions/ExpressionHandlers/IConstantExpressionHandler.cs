namespace Moon.Expressions.ExpressionHandlers
{
    public interface IConstantExpressionHandler
    { 
        ISqlExpression Handle(object? value);
    }
}