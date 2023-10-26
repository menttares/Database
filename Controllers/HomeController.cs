using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;

namespace Database.Controllers;

public class HomeController : Controller
{   

    public PostgresDataService database;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, PostgresDataService database)
    {
        _logger = logger;
        this.database = database;
    }

    [HttpGet]
    /// <summary>
    /// Метод рендеринга главной страницы
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    


    /// <summary>
    /// Метод рендеринга страницы проекта
    /// </summary>
    [HttpGet]
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
