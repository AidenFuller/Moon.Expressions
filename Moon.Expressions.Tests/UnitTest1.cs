using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.ExpressionParsers;
using System.Linq.Expressions;

namespace Moon.Expressions.Tests;

public class UnitTest1
{
    private readonly IServiceProvider _serviceProvider;

    public UnitTest1()
    {
        var services = new ServiceCollection();
        services
            .AddSingleton<IExpressionParserFactory, ExpressionParserProvider>()
            .AddSingleton<IExpressionParser, EqualExpressionParser>()
            .AddSingleton<IExpressionParser, NotEqualExpressionParser>()
            .AddSingleton<IExpressionParser, MemberAccessExpressionParser>()
            .AddSingleton<IExpressionParser, AndExpressionParser>()
            .AddSingleton<IExpressionParser, OrExpressionParser>()
            .AddSingleton<IExpressionParser, ConstantExpressionParser>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public int TTT => 10;

    [Fact]
    public void Test1()
    {
        var s = Parse<int, bool>(x => 4 == 5 || 6 == TTT);

        Console.WriteLine(s);
    }

    private string Parse<TInput, TResult>(Expression<Func<TInput, TResult>> expression)
    {
        var expressionParserProvider = _serviceProvider.GetRequiredService<IExpressionParserFactory>();
        return expressionParserProvider.GetParser(expression.Body).Parse(expression.Body);
    }
}