using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Database.Controllers;

public class ReportController : Controller
{

    public PostgresDataService database;
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger, PostgresDataService database)
    {
        _logger = logger;
        this.database = database;
    }

    public IActionResult Index()
    {
        return View();
    }

}