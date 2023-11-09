using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.Parser;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<string> Calculate([FromServices] IParser parser,
        string val1,
        string operation,
        string val2)
    {
        return parser.Parse(val1, operation, val2);
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}