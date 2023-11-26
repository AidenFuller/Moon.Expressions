using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class EqualExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public EqualExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Equal;

    public override string Evaluate(string left, string right)
    {
        return $"{left} = {right}";
    }
}
