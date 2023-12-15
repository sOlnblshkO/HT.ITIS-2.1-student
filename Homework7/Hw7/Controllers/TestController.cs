using Hw7.Models.ForTests;
using Microsoft.AspNetCore.Mvc;

namespace Hw7.Controllers;

[Controller]
public class TestController : Controller
{
    public IActionResult TestModel(TestModel model)
    {
        return View(model);
    }
}