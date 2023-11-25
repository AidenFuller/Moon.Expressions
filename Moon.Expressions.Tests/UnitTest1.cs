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

    public int? TTT => 10;

    [Fact]
    public void Test1()
    {
        var int1 = 10;
        var string1 = "10";
        object obj1 = 10;

        Assert.Equal("x.TestInt = 5", Parse<TestEntity, bool>(x => x.TestInt == 5));
        Assert.Equal("x.TestInt = 5 AND 5 = 10", Parse<TestEntity, bool>(x => x.TestInt == 5 && (5 == int1)));
        Assert.Equal("CAST(10 AS INT) = 5 AND (10 = 5 OR '10' = '10')", Parse<TestEntity, bool>(x => (int)obj1 == 5 && (int1 == 5 || string1 == "10")));
        Assert.Equal("COALESCE(10, x.TestInt) = 5", Parse<TestEntity, bool>(x => (TTT ?? x.TestInt) == 5));
        Assert.Equal("x.TestString LIKE '%' + '10' + '%'", Parse<TestEntity, bool>(x => x.TestString.Contains("10") && x.TestString.Any(y => y == '1')));
    }

    private string Parse<TInput, TResult>(Expression<Func<TInput, TResult>> expression)
    {
        var expressionParserProvider = _serviceProvider.GetRequiredService<IExpressionParserFactory>();
        return expressionParserProvider.GetParser(expression.Body, true).Parse(expression.Body);
    }

    public class TestEntity
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
        public bool TestBool { get; set; }
    }
}