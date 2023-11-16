using Microsoft.AspNetCore.Mvc;

namespace ContactManagementWeb.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}