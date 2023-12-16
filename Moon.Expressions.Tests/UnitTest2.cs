using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.DependencyInjection;
using Moon.Expressions.SqlServer.DependencyInjection;
using System.Linq.Expressions;

namespace Moon.Expressions.Tests;
public class UnitTest2
{
    [Fact]
    public void Test()
    {
        var serviceProvider = new ServiceCollection()
            .AddExpressions()
            .UseSqlServerExpressionHandlers()
            .BuildServiceProvider();

        var translator = serviceProvider.GetRequiredService<ExpressionTranslatorFactory>().GetTranslator();

        var t1 = false;
        Expression<Func<TestEntity, bool>> expression = (x) => x.TestString == "Test" && (x.TestBool == t1 || x.TestInt + 2 == 4);

        var result = translator.Translate(expression.Body);
        var parameters = result.GetAllParameters();

        result.SqlString.Should().Be("[x].[TestString] = @Value_0 AND ([x].[TestBool] = @Value_1 OR [x].[TestInt] + @Value_2 = @Value_3)");
        parameters.Should().BeEquivalentTo(new Dictionary<string, object>
        {
            ["Value_0"] = "Test",
            ["Value_1"] = false,
            ["Value_2"] = 2,
            ["Value_3"] = 4
        });      
    }

    public class TestEntity
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
        public bool TestBool { get; set; }
    }
}
