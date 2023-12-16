namespace Moon.Expressions;

public enum FullExpressionType
{
    // Binary
    And,
    Or,
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Add,
    Subtract,
    Multiply,
    Divide,
    Coalesce,
    Conditional,

    // Unary
    Not,
    Cast,

    // Method Call
    Contains,
    StartsWith,
    EndsWith,
    In,
    Between,

    // Constant
    Null,
    ConstantValue,

    // Members
    Parameter,
    ParameterMember,
    RuntimeVariable,
}

public static class ExpressionTypeExtensions
{
    public static bool ShouldWrapInBrackets(this FullExpressionType childType, FullExpressionType parentType)
    {
        return
            (parentType == FullExpressionType.And && childType == FullExpressionType.Or) ||
            (parentType == FullExpressionType.Or && childType == FullExpressionType.And) ||
            (parentType.IsNumericOperation() && childType == FullExpressionType.Subtract) ||
            (parentType.IsNumericOperation() && childType == FullExpressionType.Divide);
    }

    public static bool IsNumericOperation(this FullExpressionType type) => type switch
    {
        FullExpressionType.Add or FullExpressionType.Subtract or FullExpressionType.Multiply or FullExpressionType.Divide => true,
        _ => false
    };
}