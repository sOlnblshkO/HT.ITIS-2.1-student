using System.Diagnostics.CodeAnalysis;
using Hw9.Configuration;
using Hw9.Services.ExpressionValidator;
using Hw9.Services.ExpressionParser;
using Hw9.Services.MyExpressionVisitor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddMathCalculator();

builder.Services.AddSingleton<IExpressionValidator, ExpressionValidator>();
builder.Services.AddSingleton<IExpressionParser, ExpressionParser>();
builder.Services.AddSingleton<IExpressionVisitor, MyExpressionVisitor>();

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
    name: "default",
    pattern: "{controller=Calculator}/{action=Calculator}/{id?}");

app.Run();

namespace Hw9
{
    [ExcludeFromCodeCoverage]
    public partial class Program { }
}