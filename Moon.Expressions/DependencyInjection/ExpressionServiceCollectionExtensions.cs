using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.ExpressionParsers;

namespace Moon.Expressions.DependencyInjection
{
    public static class ExpressionServiceCollectionExtensions
    {
        public static IServiceCollection AddExpressionParsers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IExpressionParserFactory, ExpressionParserFactory>()
                .AddSingleton<IConstantResolver, ConstantResolver>();
        }
    }
}
