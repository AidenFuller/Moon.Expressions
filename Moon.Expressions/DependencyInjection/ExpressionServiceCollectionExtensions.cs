using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.DependencyInjection
{
    public static class ExpressionServiceCollectionExtensions
    {
        public static IServiceCollection AddExpressionParsers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IExpressionParserProvider, ExpressionParserProvider>()
                .Scan(scan => scan
                    .FromExecutingAssembly()
                    .AddClasses(classes => classes.AssignableTo<IExpressionParser>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
        }
    }
}
