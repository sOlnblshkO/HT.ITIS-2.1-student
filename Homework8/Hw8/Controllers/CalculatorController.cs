using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        try
        {
            Parser.ParseCalcArguments(new[] { val1, operation, val2 },
                out var arg1, out var op, out var arg2);

            return op switch
            {
                Operation.Plus => calculator.Plus(arg1, arg2),
                Operation.Minus => calculator.Minus(arg1, arg2),
                Operation.Multiply => calculator.Multiply(arg1, arg2),
                Operation.Divide => calculator.Divide(arg1, arg2),
                _ => BadRequest(Messages.InvalidOperationMessage)
            };
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}