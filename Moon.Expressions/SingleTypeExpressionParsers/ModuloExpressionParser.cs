using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class ModuloExpressionParser : BinaryExpressionParserBase, ISingleTypeExpressionParser
{
    public ModuloExpressionParser(IExpressionParserProvider expressionParserProvider) : base(expressionParserProvider)
    {
    }

    public ExpressionType ExpressionType => ExpressionType.Modulo;

    public override string Evaluate(string left, string right)
    {
        return $"MOD({left}, {right})";
    }
}
