using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class GreaterThanExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public GreaterThanExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.GreaterThan;

    public override string Evaluate(string left, string right)
    {
        return $"{left} > {right}";
    }
}
