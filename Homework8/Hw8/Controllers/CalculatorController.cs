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
            return Parser.ParseArguments(val1, operation, val2)switch
            {
                (var firstValue, Operation.Plus, var secondValue) => calculator.Plus(firstValue, secondValue),
                (var firstValue, Operation.Minus, var secondValue) => calculator.Minus(firstValue, secondValue),
                (var firstValue, Operation.Multiply, var secondValue) => calculator.Multiply(firstValue, secondValue),
                (var firstValue, Operation.Divide, 0) => Content(Messages.DivisionByZeroMessage),
                (var firstValue, Operation.Divide, var secondValue) => calculator.Divide(firstValue, secondValue),
            };
        }
        catch (ArgumentException)
        {
            return Content(Messages.InvalidNumberMessage);
        }
        catch
        {
            return Content(Messages.InvalidOperationMessage);
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