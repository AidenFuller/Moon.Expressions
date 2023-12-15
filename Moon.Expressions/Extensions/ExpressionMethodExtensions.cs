namespace Moon.Expressions.Extensions;

public static class ExpressionMethodExtensions
{
    public static bool In(this object value, params object[] values)
    {
        return values.Contains(value);
    }

    public static bool Between(this IComparable value, object min, object max)
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }
}