using Moon.Expressions.ExpressionParsers;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserFactory
{
    IExpressionParser GetParser(ExpressionType expressionType, bool isRoot = false);
}

public class ExpressionParserFactory : IExpressionParserFactory
{
    private readonly IConstantResolver _constantResolver;

    public ExpressionParserFactory(IConstantResolver constantResolver)
    {
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
    }

    public IExpressionParser GetParser(ExpressionType expressionType, bool isRoot = false) => expressionType switch
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


        _ => throw new NotSupportedException($"Expression type {expressionType} is not supported.")
    };
}