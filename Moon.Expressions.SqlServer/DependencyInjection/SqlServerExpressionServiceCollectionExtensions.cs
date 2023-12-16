using Microsoft.Extensions.DependencyInjection;
using Moon.Expressions.ExpressionHandlers;

namespace Moon.Expressions.SqlServer.DependencyInjection;
public static class SqlServerExpressionServiceCollectionExtensions
{
    public static IServiceCollection UseSqlServerExpressionHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IExpressionHandler>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
