using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.Controllers;
using Hw8.MyMiddlewares;
using Hw8.Services;
using Hw8.Services.CalculatorServices;

namespace Hw8;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<ICalculator, CalculatorService>();
        builder.Services.AddScoped<ICalculatorParser<CalculatorOptions>, CalculatorParserService>();
        builder.Services.AddSingleton<MyExceptionHandler>();
        
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseMiddleware<MyExceptionHandler>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Calculator}/{action=Index}");

        app.Run();
    }
}