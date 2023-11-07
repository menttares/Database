using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetFilter(String tableName)
    {
        return PartialView($"filter-{tableName}");
    }

    [HttpPost]
    public IActionResult PostFilterOrder([FromForm] FilterOrderModel filter)
    {
        var result = database.FilterOrders(filter);
        var jsonResult = JsonConvert.SerializeObject(result, new DataTableConverter());
        return Content(jsonResult, "application/json");
    }

    [HttpGet]
    public IActionResult GetForm(String tableName)
    {
        var Customers = database.GetTable("Покупатель");
        var Furniture = database.GetTable("Мебель");

        List<CustomerModel> customers = new List<CustomerModel>();
        foreach (DataRow row in Customers.Rows)
        {

            var customer = new CustomerModel
            {
                ID = int.Parse(row["Id"].ToString()),
                ФИО = row["ФИО"].ToString(),
                Телефон = row["Телефон"].ToString()
            };
            customers.Add(customer);
        }
        List<FurnitureModel> furnitures = new List<FurnitureModel>();
        foreach (DataRow row in Furniture.Rows)
        {

            var furniture = new FurnitureModel
            {
                ID = int.Parse(row["Id"].ToString()),
                Название = row["Название"].ToString(),
                Цена = decimal.Parse(row["Цена"].ToString())
            };
            furnitures.Add(furniture);
        }

        ViewData["customers"] = customers;
        ViewData["furnitures"] = furnitures;

        return PartialView($"form-{tableName}");
    }

    [HttpPost]
    public IActionResult PostForm([FromQuery] String tableName, [FromForm] OrderModel order)
    {
        var result = database.AddOrder(order);
        var jsonResult = JsonConvert.SerializeObject(result, new DataTableConverter());
        return Ok(jsonResult);
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
