using Hw11.Services.ExpressionParser;
using Hw11.Services.ExpressionValidator;
using Hw11.Services.MathCalculator;
using Hw11.Services.MyExpressionVisitor;

namespace Hw11.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services.AddTransient<IMathCalculatorService, MathCalculatorService>()
            .AddTransient<IExpressionValidator, ExpressionValidator>()
            .AddTransient<IExpressionParser, ExpressionParser>()
            .AddTransient<IExpressionVisitor, MyExpressionVisitor>();
    }
}