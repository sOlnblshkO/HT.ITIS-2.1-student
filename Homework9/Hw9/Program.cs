using System.Diagnostics.CodeAnalysis;
using Hw9.Configuration;
using Hw9.Services.ExpressionCalculator;
using Hw9.Services.ExpressionParser;
using Hw9.Services.ExpressionTokenizer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddMathCalculator();

builder.Services.AddSingleton<IExpressionTokenizer, MathExpressionTokenizer>();
builder.Services.AddSingleton<IExpressionParser, MathExpressionParser>();
builder.Services.AddSingleton<IExpressionCalculator, MathExpressionCalculator>();

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
    "{controller=Calculator}/{action=Calculator}/{id?}");

app.Run();

namespace Hw9
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
    }
}