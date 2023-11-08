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
                out var value1,
                out var operation1,
                out var value2);

            return operation1 switch
            {
                Operation.Plus => calculator.Plus(value1, value2),
                Operation.Minus => calculator.Minus(value1, value2),
                Operation.Multiply => calculator.Multiply(value1, value2),
                Operation.Divide => value2 != 0 ? calculator.Divide(value1, value2) : this.Content(Messages.DivisionByZeroMessage),
                _ => this.Content(Messages.InvalidOperationMessage)
            };
        }
        catch (ArgumentException)
        {
            return this.Content(Messages.InvalidNumberMessage);
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