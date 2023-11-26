using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class AndAlsoExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public AndAlsoExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.AndAlso;

    public override string Evaluate(string left, string right)
    {
        return $"({left} AND {right})";
    }
}
