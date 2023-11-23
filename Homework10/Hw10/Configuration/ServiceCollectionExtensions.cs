using Hw10.DbModels;
using Hw10.Services;
using Hw10.Services.CachedCalculator;
using Hw10.Services.MathCalculator;
using Hw10.Services.MathCalculator.ExpressionCalculator;
using Hw10.Services.MathCalculator.ExpressionParser;
using Hw10.Services.MathCalculator.ExpressionTokenizer;

namespace Hw10.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services.AddSingleton<IExpressionTokenizer, MathExpressionTokenizer>()
            .AddSingleton<IExpressionParser, MathExpressionParser>()
            .AddSingleton<IExpressionCalculator, MathExpressionCalculator>()
            .AddTransient<MathCalculatorService>();
    }

    public static IServiceCollection AddCachedMathCalculator(this IServiceCollection services)
    {
        return services.AddScoped<IMathCalculatorService>(s =>
            new MathCachedCalculatorService(
                s.GetRequiredService<ApplicationContext>(),
                s.GetRequiredService<MathCalculatorService>()));
    }
}