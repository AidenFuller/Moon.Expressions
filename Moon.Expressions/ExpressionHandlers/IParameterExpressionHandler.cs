namespace Moon.Expressions.ExpressionHandlers;

public interface IParameterExpressionHandler : IExpressionHandler
{
    SqlExpression Handle(string parameterName);
}
