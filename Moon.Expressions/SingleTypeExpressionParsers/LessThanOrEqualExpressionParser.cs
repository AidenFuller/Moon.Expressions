using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class LessThanOrEqualExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public LessThanOrEqualExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.LessThanOrEqual;

    public override string Evaluate(string left, string right)
    {
        return $"{left} <= {right}";
    }
}
