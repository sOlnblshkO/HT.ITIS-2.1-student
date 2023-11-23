using Hw10.DbModels;
using Hw10.Services;
using Hw10.Services.CachedCalculator;
using Hw10.Services.ExpressionParser;
using Hw10.Services.ExpressionValidator;
using Hw10.Services.MathCalculator;
using Hw10.Services.MyExpressionVisitor;

namespace Hw10.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services.AddTransient<MathCalculatorService>()
            .AddTransient<IExpressionValidator, ExpressionValidator>()
            .AddTransient<IExpressionParser, ExpressionParser>()
            .AddTransient<IExpressionVisitor, MyExpressionVisitor>();
    }
    
    public static IServiceCollection AddCachedMathCalculator(this IServiceCollection services)
    {
        return services.AddScoped<IMathCalculatorService>(s =>
            new MathCachedCalculatorService(
                s.GetRequiredService<ApplicationContext>(), 
                s.GetRequiredService<MathCalculatorService>()));
    }
}