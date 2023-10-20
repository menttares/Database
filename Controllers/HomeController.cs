using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;

namespace Database.Controllers;

public class HomeController : Controller
{   

    public DataAccess database;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, DataAccess database)
    {
        _logger = logger;
        this.database = database;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
