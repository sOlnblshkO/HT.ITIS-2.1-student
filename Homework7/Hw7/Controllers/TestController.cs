using Hw7.Models.ForTests;
using Microsoft.AspNetCore.Mvc;

namespace Hw7.Controllers;

[Controller]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult TestModel()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TestModel(TestModel model)
    {
        return View(model);
    }
}