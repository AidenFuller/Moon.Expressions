using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class GreaterThanOrEqualExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public GreaterThanOrEqualExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.GreaterThanOrEqual;

    public override string Evaluate(string left, string right)
    {
        return $"{left} >= {right}";
    }
}
