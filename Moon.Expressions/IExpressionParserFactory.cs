using Moon.Expressions.ExpressionParsers;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserFactory
{
    IExpressionParser GetParser(Expression expression, bool isRoot = false);
}

public class ExpressionParserFactory : IExpressionParserFactory
{
    private readonly IConstantResolver _constantResolver;

    public ExpressionParserFactory(IConstantResolver constantResolver)
    {
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
    }

    public IExpressionParser GetParser(Expression expression, bool isRoot = false) => expression.NodeType switch
    {
        ExpressionType.AndAlso => new SimpleBinaryExpressionParser(this, "AND", !isRoot),
        ExpressionType.OrElse => new SimpleBinaryExpressionParser(this, "OR", !isRoot),
        ExpressionType.Equal => new SimpleBinaryExpressionParser(this, "="),
        ExpressionType.NotEqual => new SimpleBinaryExpressionParser(this, "<>"),
        ExpressionType.GreaterThan => new SimpleBinaryExpressionParser(this, ">"),
        ExpressionType.GreaterThanOrEqual => new SimpleBinaryExpressionParser(this, ">="),
        ExpressionType.LessThan => new SimpleBinaryExpressionParser(this, "<"),
        ExpressionType.LessThanOrEqual => new SimpleBinaryExpressionParser(this, "<="),

        ExpressionType.Add => new SimpleBinaryExpressionParser(this, "+"),
        ExpressionType.Subtract => new SimpleBinaryExpressionParser(this, "-"),
        ExpressionType.Multiply => new SimpleBinaryExpressionParser(this, "*"),
        ExpressionType.Divide => new SimpleBinaryExpressionParser(this, "/"),
        ExpressionType.Modulo => new FunctionBinaryExpressionParser(this, "MOD"),
        ExpressionType.Coalesce => new FunctionBinaryExpressionParser(this, "COALESCE"),

        ExpressionType.Convert => new ConversionExpressionParser(this),

        ExpressionType.Constant => new ConstantExpressionParser(_constantResolver),
        ExpressionType.MemberAccess => new MemberAccessExpressionParser(this, _constantResolver),

        ExpressionType.Parameter => new ParameterExpressionParser(),

        ExpressionType.Call => GetCallExpressionParser((MethodCallExpression)expression),

        _ => throw new NotSupportedException($"Expression type {expression.NodeType} is not supported.")
    };

    private IExpressionParser GetCallExpressionParser(MethodCallExpression expression)
    {
        var callerType = expression.Object?.Type;
        var calledMethod = expression.Method.Name;

        if (callerType == typeof(string) && calledMethod == nameof(string.Contains)) return new StringContainsExpressionParser(this);
        if (callerType == typeof(string) && calledMethod == nameof(string.StartsWith)) return new StringStartsWithExpressionParser(this);
        if (callerType == typeof(string) && calledMethod == nameof(string.EndsWith)) return new StringEndsWithExpressionParser(this);
        throw new NotSupportedException($"Call Expression with caller type {callerType.Name} and method {calledMethod} is not supported.");
    }
}