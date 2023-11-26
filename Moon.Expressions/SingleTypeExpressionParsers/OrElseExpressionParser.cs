using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class OrElseExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public OrElseExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.OrElse;

    public override string Evaluate(string left, string right)
    {
        return $"({left} OR {right})";
    }
}
