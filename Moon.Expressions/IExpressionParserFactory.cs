using Moon.Expressions.ExpressionParsers;
using System.Linq.Expressions;

namespace Moon.Expressions;

public interface IExpressionParserFactory
{
    IExpressionParser GetParser(ExpressionType expressionType);
}

public class ExpressionParserFactory : IExpressionParserFactory
{
    private readonly IConstantResolver _constantResolver;

    public ExpressionParserFactory(IConstantResolver constantResolver)
    {
        _constantResolver = constantResolver ?? throw new ArgumentNullException(nameof(constantResolver));
    }

    public IExpressionParser GetParser(ExpressionType expressionType) => expressionType switch
    {
        ExpressionType.AndAlso => new SimpleBinaryExpressionParser(this, "AND"),
        ExpressionType.OrElse => new SimpleBinaryExpressionParser(this, "OR"),
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

        ExpressionType.Constant => new ConstantExpressionParser(_constantResolver),
        ExpressionType.MemberAccess => new MemberAccessExpressionParser(this, _constantResolver),


        _ => throw new NotSupportedException($"Expression type {expressionType} is not supported.")
    };
}