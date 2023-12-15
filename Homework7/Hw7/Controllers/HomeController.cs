using Hw7.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hw7.Controllers;

[Controller]
public class HomeController : Controller
{
    public IActionResult UserProfile(UserProfile model)
    {
        return View(model);
    }
}