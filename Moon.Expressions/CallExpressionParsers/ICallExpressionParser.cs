namespace Moon.Expressions.CallExpressionParsers;

public interface ICallExpressionParser : IExpressionParser
{
    CallExpressionType CallExpressionType { get; }
}
