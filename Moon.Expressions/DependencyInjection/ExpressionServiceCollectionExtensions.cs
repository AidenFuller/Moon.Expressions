using Microsoft.Extensions.DependencyInjection;

namespace Moon.Expressions.DependencyInjection;

public static class ExpressionServiceCollectionExtensions
{
    private static IServiceCollection AddAllImplementationsInAssembly<TService>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblyOf<TService>()
            .AddClasses(classes => classes.AssignableTo<TService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    public static IServiceCollection AddExpressions(this IServiceCollection services)
    {
        return services
            .AddSingleton<ExpressionTranslatorFactory>()
            .AddSingleton<IExpressionTypeCalculator, ExpressionTypeCalculator>()
            .AddScoped<ExpressionTranslator>()
            .AddScoped<ExpressionParameterProvider>();
    }
}
