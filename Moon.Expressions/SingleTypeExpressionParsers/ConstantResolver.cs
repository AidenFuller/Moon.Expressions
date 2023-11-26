namespace Moon.Expressions.SingleTypeExpressionParsers;

public interface IConstantResolver
{
    string Resolve(object? value);
}

public class ConstantResolver : IConstantResolver
{
    public string Resolve(object? value) => value switch
    {
        null => "NULL",
        char c => $"'{c}'",
        string text => $"'{text}'",
        bool bit => bit ? "1" : "0",
        int i => i.ToString(),
        decimal dec => $"{dec:D}",
        double dbl => dbl.ToString(),
        DateTime dt => $"'{dt:yyyy-MM-dd hh:mm:ss}'",
        DateOnly dateOnly => $"'{dateOnly:yyyy-MM-dd}'",
        _ => throw new NotSupportedException("The type of this constant is not supported")
    };
}
