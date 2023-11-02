using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;
using System.Reflection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Database.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DBController : ControllerBase
{
    public PostgresDataService database;
    private readonly ILogger<DBController> _logger;

    public DBController(ILogger<DBController> logger, PostgresDataService database)
    {
        _logger = logger;
        this.database = database;
    }

    [HttpGet("methods")]
    public IActionResult GetMethods()
    {
        var controllerType = typeof(DBController);
        var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        var methodNames = methods
            .Where(m => m.DeclaringType == controllerType)
            .Select(m => m.Name)
            .ToList();

        return Ok(methodNames);
    }


    /// <summary>
    /// Получение данных из определенной таблицы базы данных
    /// </summary>
    /// <param name="tableName">название таблицы</param>
    /// <param name="countRow">количество строк, при null возвращяет все строки</param>
    /// <returns>Возвращает JsonContent</returns>
    [HttpGet("getTable")]
    public IActionResult GetTable(String tableName)
    {
        _logger.LogInformation($"[Api] - входящий запрос на GetTable({tableName})");

        String sql = string.Empty;

        var result = database.GetTable(tableName);
        var jsonResult = JsonConvert.SerializeObject(result, new DataTableConverter());


        return Content(jsonResult, "application/json");
    }

    [HttpGet("columsTable")]
    public IActionResult GetColumsTable(String tableName)
    {
        _logger.LogInformation($"[Api] - входящий запрос на GetColumsTable({tableName})");
        if (string.IsNullOrEmpty(tableName))
        {
            return BadRequest("Table name is required.");
        }

        var result = database.ColumsTable(tableName);


        var jsonResult = JsonConvert.SerializeObject(result, new DataTableConverter());

        return Content(jsonResult, "application/json");
    }


    // [HttpGet("getView")]
    // public IActionResult GetView(String tableName)
    // {
    //     _logger.LogInformation("[Api] - входящий запрос на GetTable()");

    //     String sql = string.Empty;

    //     var result = database.GetTable(tableName);
    //     var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented, new DataTableConverter());


    //     return Content(jsonResult, "application/json");
    // }
}