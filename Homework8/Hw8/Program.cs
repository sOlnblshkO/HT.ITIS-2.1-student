using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;

namespace Hw8;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<ICalculator, DoubleCalculator>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Calculator}/{action=Index}");

        app.Run();
    }
}