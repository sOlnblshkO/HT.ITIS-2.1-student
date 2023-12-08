using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.Services.CalculatorServices;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    private readonly ICalculatorParser<CalculatorOptions> _calculatorParser;
    
    public CalculatorController(ICalculatorParser<CalculatorOptions> calculatorParser)
    {
        _calculatorParser = calculatorParser;
    }
    
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        var calcOptions = _calculatorParser.ParseCalculatorArguments(new string[] { val1, operation, val2 });
        
        return calcOptions.Operation switch
        {
            Operation.Plus => calculator.Plus(calcOptions.Value1, calcOptions.Value2),
            Operation.Minus => calculator.Minus(calcOptions.Value1, calcOptions.Value2),
            Operation.Multiply => calculator.Multiply(calcOptions.Value1, calcOptions.Value2),
            Operation.Divide => calculator.Divide(calcOptions.Value1, calcOptions.Value2),
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}