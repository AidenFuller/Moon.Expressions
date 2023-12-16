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
            .AddVisitorPattern()
            .UseSqlServerExpressionHandlers()
            .BuildServiceProvider();

        var translator = serviceProvider.GetRequiredService<ExpressionTranslatorFactory>().GetTranslator();

        var t1 = false;
        Expression<Func<TestEntity, bool>> expression = (x) => x.TestString == "Test" && (x.TestBool == t1 || x.TestInt + 2 == 4);

        var result = translator.Translate(expression.Body);
        var parameters = result.GetAllParameters();

        result.SqlString.Should().Be("@Value_1 = @Value_2");
        parameters.Keys.Should().Contain("Value_1");
        parameters.Keys.Should().Contain("Value_2");

        parameters["Value_1"].Should().Be(false);
        parameters["Value_2"].Should().Be(true);        
    }

    public class TestEntity
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
        public bool TestBool { get; set; }
    }
}
