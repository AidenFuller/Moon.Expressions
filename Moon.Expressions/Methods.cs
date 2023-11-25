using System.Reflection;

namespace Moon.Expressions;

public static class Methods
{
    public readonly static MethodInfo StringConcat = ((Func<string, string, string>)string.Concat).Method;
}
