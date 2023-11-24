using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.DependencyInjection;
using System.Linq.Expressions;

namespace Moon.Expressions.Tests;

public class UnitTest1
{
    private readonly IServiceProvider _serviceProvider;

    public UnitTest1()
    {
        var services = new ServiceCollection();
        services.AddExpressionParsers();

        _serviceProvider = services.BuildServiceProvider();
    }

    public int TTT => 10;

    [Fact]
    public void Test1()
    {
        var xxx = 10;
        var s = Parse<int, bool>(x => TTT == 5 || 6 == xxx);

        Console.WriteLine(s);
    }

    private string Parse<TInput, TResult>(Expression<Func<TInput, TResult>> expression)
    {
        var expressionParserProvider = _serviceProvider.GetRequiredService<IExpressionParserProvider>();
        return expressionParserProvider.GetParser(expression.Body).Parse(expression.Body);
    }
}