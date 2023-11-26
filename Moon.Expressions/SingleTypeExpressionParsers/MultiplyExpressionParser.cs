using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class MultiplyExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public MultiplyExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Multiply;

    public override string Evaluate(string left, string right)
    {
        return $"{left} * {right}";
    }
}
