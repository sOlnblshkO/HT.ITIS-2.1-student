using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.Services.CalculatorServices;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    private readonly ICalculatorParser<UnparsedCalculatorOptions, CalculatorOptions> _calculatorParser;
    
    public CalculatorController(ICalculatorParser<UnparsedCalculatorOptions, CalculatorOptions> calculatorParser)
    {
        _calculatorParser = calculatorParser;
    }
    
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        var unparsedCalcOptions = new UnparsedCalculatorOptions(val1, operation, val2);
        var calcOptions = _calculatorParser.ParseCalculatorArguments(unparsedCalcOptions);
        
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