using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class LessThanExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public LessThanExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.LessThan;

    public override string Evaluate(string left, string right)
    {
        return $"{left} < {right}";
    }
}
