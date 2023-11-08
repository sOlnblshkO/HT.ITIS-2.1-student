using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.Parser;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator, [FromServices] IParser parser,
        string val1,
        string operation,
        string val2)
    {
        if (parser.ParseArgument(val1, out var value1) && parser.ParseArgument(val2, out var value2))
        {
            try
            {
                return parser.ParseOperation(operation) switch
                {
                    Operation.Plus => calculator.Plus(value1, value2),
                    Operation.Minus => calculator.Minus(value1, value2),
                    Operation.Multiply => calculator.Multiply(value1, value2),
                    Operation.Divide => calculator.Divide(value1, value2),
                    _ => BadRequest(Messages.InvalidOperationMessage)
                };
            }
            catch(InvalidOperationException)
            {
                return BadRequest(Messages.DivisionByZeroMessage);
            }

        }
        return BadRequest(Messages.InvalidNumberMessage);
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}