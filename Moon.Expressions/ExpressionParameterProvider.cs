namespace Moon.Expressions;

public class ExpressionParameterProvider
{
    private int _index = 0;

    public string GetUniqueParameterName(string name)
    {
        return $"{name}_{_index++}";
    }
}
