using Microsoft.Extensions.DependencyInjection;

namespace Moon.Expressions;
public class ExpressionTranslatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExpressionTranslatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public ExpressionTranslator GetTranslator()
    {
        using var scope = _serviceProvider.CreateScope();
        return _serviceProvider.GetRequiredService<ExpressionTranslator>();
    }
}
