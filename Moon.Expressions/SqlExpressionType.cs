public enum BinaryExpressionType
{
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
    Conditional
}

public enum UnaryExpressionType
{
    Not,
    Cast
}

public enum MethodCallExpressionType
{
    Contains,
    StartsWith,
    EndsWith,
    In,
    Between
}

public enum ConstantExpressionType
{
    Null,
    Value
}


public static class BinaryExpressionTypeExtensions
{
    public static bool OrderMatters(this BinaryExpressionType expressionType)
    {
        return expressionType == BinaryExpressionType.And || expressionType == BinaryExpressionType.Or;
    }
}