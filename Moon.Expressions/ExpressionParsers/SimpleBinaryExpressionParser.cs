using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.ExpressionParsers;

public abstract class BinaryExpressionParserBase : IExpressionParser
{
    private readonly IExpressionParserFactory _expressionParserFactory;

    public BinaryExpressionParserBase(IExpressionParserFactory expressionParserProvider)
    {
        _expressionParserFactory = expressionParserProvider ?? throw new ArgumentNullException(nameof(expressionParserProvider));
    }

    public string Parse(Expression expression)
    {
        var binaryExpression = (BinaryExpression)expression;

        var left = _expressionParserFactory.ResolveAndParse(binaryExpression.Left);
        var right = _expressionParserFactory.ResolveAndParse(binaryExpression.Right);

        return Evaluate(left, right);
    }

    public abstract string Evaluate(string left, string right);
}

public class SimpleBinaryExpressionParser : BinaryExpressionParserBase
{
    private readonly string _operation;
    private readonly bool _wrappingBrackets;

    public SimpleBinaryExpressionParser(IExpressionParserFactory expressionParserProvider, string operation, bool wrappingBrackets = false) : base(expressionParserProvider)
    {
        _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        _wrappingBrackets = wrappingBrackets;
    }

    public override string Evaluate(string left, string right)
    {
        return Wrap($"{left} {_operation} {right}");
    }

    private string Wrap(string text) => _wrappingBrackets ? $"({text})" : text;
}

public class FunctionBinaryExpressionParser : BinaryExpressionParserBase
{
    private readonly string _functionName;

    public FunctionBinaryExpressionParser(IExpressionParserFactory expressionParserFactory, string functionName) : base(expressionParserFactory)
    {
        _functionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
    }

    public override string Evaluate(string left, string right)
    {
        return $"{_functionName}({left}, {right})";
    }
}