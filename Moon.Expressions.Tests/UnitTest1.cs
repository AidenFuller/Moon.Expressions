using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.DependencyInjection;
using Moon.Expressions.Extensions;
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
        Assert.Equal("(x.TestInt = 5 AND 5 = 10)", Parse<TestEntity, bool>(x => x.TestInt == 5 && (5 == int1)));
        Assert.Equal("(CAST(10 AS INT) = 5 AND (10 = 5 OR '10' = '10'))", Parse<TestEntity, bool>(x => (int)obj1 == 5 && (int1 == 5 || string1 == "10")));
        Assert.Equal("ISNULL(10, x.TestInt) = 5", Parse<TestEntity, bool>(x => (TTT ?? x.TestInt) == 5));
        Assert.Equal("(x.TestString LIKE '%' + '10' + '%' AND 1 IN (1, 2, 3))", Parse<TestEntity, bool>(x => x.TestString.Contains("10") && new[] { 1, 2, 3 }.Contains(1)));
        Assert.Equal("x.TestInt IN (1, 10, 3)", Parse<TestEntity, bool>(x => new[] { 1, int1, 3 }.Contains(x.TestInt)));
        Assert.Equal("x.TestDate BETWEEN '2022-01-01' AND '2022-06-01'", Parse<TestEntity, bool>(x => x.TestDate.IsBetween(new DateOnly(2022, 01, 01), new DateOnly(2022, 06, 01))));
    }

    private string Parse<TInput, TResult>(Expression<Func<TInput, TResult>> expression)
    {
        var expressionParserProvider = _serviceProvider.GetRequiredService<IExpressionParserProvider>();
        return expressionParserProvider.ResolveAndParse(expression.Body);
    }

    public class TestEntity
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
        public bool TestBool { get; set; }
        public DateOnly TestDate { get; set; }
    }
}