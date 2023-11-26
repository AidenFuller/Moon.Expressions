using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class CoalesceExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public CoalesceExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Coalesce;

    public override string Evaluate(string left, string right)
    {
        return $"ISNULL({left}, {right})";
    }
}
