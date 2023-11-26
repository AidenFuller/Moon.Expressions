using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class AddExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public AddExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Add;

    public override string Evaluate(string left, string right)
    {
        return $"{left} + {right}";
    }
}
