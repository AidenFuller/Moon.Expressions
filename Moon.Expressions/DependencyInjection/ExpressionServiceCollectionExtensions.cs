using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.SingleTypeExpressionParsers;

namespace Moon.Expressions.DependencyInjection;

public static class ExpressionServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionParsers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IExpressionParserProvider, ExpressionParserProvider>()
            .AddSingleton<IConstantResolver, ConstantResolver>()
            .AddSingleton<ICallExpressionTypeEvaluator, CallExpressionTypeEvaluator>()
            .AddAllImplementationsInAssembly<IExpressionParser>()
            .AddAllImplementationsInAssembly<IExpressionParserFactory>();
    }

    private static IServiceCollection AddAllImplementationsInAssembly<TService>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblyOf<TService>()
            .AddClasses(classes => classes.AssignableTo<TService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    public static IServiceCollection AddVisitorPattern(this IServiceCollection services)
    {
        return services
            .AddSingleton<ExpressionTranslatorFactory>()
            .AddSingleton<IExpressionTypeCalculator, ExpressionTypeCalculator>()
            .AddScoped<ExpressionTranslator>()
            .AddScoped<ExpressionParameterProvider>();
    }
}
