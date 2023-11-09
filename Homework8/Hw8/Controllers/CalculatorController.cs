using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.Services;
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
            return Ok(calculator.Calculate(val1, operation, val2));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch 
        {
            return StatusCode(500);
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