using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class NotEqualExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public NotEqualExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.NotEqual;

    public override string Evaluate(string left, string right)
    {
        return $"{left} <> {right}";
    }
}
