using Moon.Expressions.Extensions;
using System.Linq.Expressions;

namespace Moon.Expressions.SingleTypeExpressionParsers;

public class ConversionExpressionParser : ISingleTypeExpressionParser
{
    private readonly IExpressionParserProvider _expressionParserFactory;

    public ExpressionType ExpressionType => ExpressionType.Convert;

    public ConversionExpressionParser(IExpressionParserProvider expressionParserFactory)
    {
        _expressionParserFactory = expressionParserFactory;
    }

    public string Parse(Expression expression)
    {
        var unaryExpression = (UnaryExpression)expression;
        var castType = Nullable.GetUnderlyingType(unaryExpression.Type) ?? unaryExpression.Type;
        var operandType = Nullable.GetUnderlyingType(unaryExpression.Operand.Type) ?? unaryExpression.Operand.Type;

        if (castType != operandType)
        {
            return $"CAST({_expressionParserFactory.ResolveAndParse(unaryExpression.Operand)} AS {GetCastType(castType)})";
        }
        else
        {
            return _expressionParserFactory.ResolveAndParse(unaryExpression.Operand);
        }
    }

    private static string GetCastType(Type type)
    {
        if (type == typeof(int)) return "INT";
        if (type == typeof(long)) return "BIGINT";
        if (type == typeof(bool)) return "BIT";
        if (type == typeof(DateTime)) return "DATETIME";
        if (type == typeof(DateTimeOffset)) return "DATETIMEOFFSET";
        if (type == typeof(DateOnly)) return "DATE";
        if (type == typeof(decimal)) return "DECIMAL(18,6)";
        if (type == typeof(double)) return "FLOAT";
        if (type == typeof(Guid)) return "UNIQUEIDENTIFIER";
        if (type == typeof(string)) return "NVARCHAR(MAX)";
        if (type == typeof(char)) return "CHAR";
        throw new NotImplementedException($"Type {type.Name} not supported for conversion");
    }
}
