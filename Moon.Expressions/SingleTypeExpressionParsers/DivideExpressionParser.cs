using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class DivideExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public DivideExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Divide;

    public override string Evaluate(string left, string right)
    {
        return $"{left} / {right}";
    }
}
