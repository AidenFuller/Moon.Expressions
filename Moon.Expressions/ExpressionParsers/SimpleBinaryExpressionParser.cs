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

        var left = _expressionParserFactory.GetParser(binaryExpression.Left.NodeType).Parse(binaryExpression.Left);
        var right = _expressionParserFactory.GetParser(binaryExpression.Right.NodeType).Parse(binaryExpression.Right);

        return Evaluate(left, right);
    }

    public abstract string Evaluate(string left, string right);
}

public class SimpleBinaryExpressionParser : BinaryExpressionParserBase
{
    private readonly string _operation;

    public SimpleBinaryExpressionParser(IExpressionParserFactory expressionParserProvider, string operation) : base(expressionParserProvider)
    {
        _operation = operation ?? throw new ArgumentNullException(nameof(operation));
    }

    public override string Evaluate(string left, string right)
    {
        return $"({left}) {_operation} ({right})";
    }
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