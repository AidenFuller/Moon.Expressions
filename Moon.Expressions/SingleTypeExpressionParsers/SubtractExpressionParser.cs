using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class SubtractExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public SubtractExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Subtract;

    public override string Evaluate(string left, string right)
    {
        return $"{left} - {right}";
    }
}
