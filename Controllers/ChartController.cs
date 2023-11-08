using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Database.Controllers;

public class ChartController : Controller
{

    public PostgresDataService database;
    private readonly ILogger<ChartController> _logger;

    public ChartController(ILogger<ChartController> logger, PostgresDataService database)
    {
        _logger = logger;
        this.database = database;
    }

    public IActionResult Index()
    {
        return View();
    }

}