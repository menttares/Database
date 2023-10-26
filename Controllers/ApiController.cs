using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database.Services;
using Database.Models;

namespace Database.Controllers;

[Route("api/[controller]")]
[ApiController]
class ApiController : ControllerBase
{
    public PostgresDataService database;
    private readonly ILogger<ApiController> _logger;

    public ApiController(ILogger<ApiController> logger, PostgresDataService database)
    {
        _logger = logger;
        this.database = database;
    }

    /// <summary>
    /// Получение данных из определенной таблицы базы данных
    /// </summary>
    /// <param name="tableName">название таблицы</param>
    /// <param name="countRow">количество строк, при null возвращяет все строки</param>
    /// <returns>Возвращает JsonContent</returns>
    [HttpGet("table")]
    public IActionResult GetTable(String tableName, int? countRow)
    {
        
        return Ok("Data from table");
    }

    [HttpPost]
    public IActionResult PostRowTable()
    {
        return Ok("Data from table");
    }

    [HttpDelete]
    public IActionResult PostDeleteTable(int Id)
    {

        return Ok("Data from table");
    }

    [HttpPut]
    public IActionResult PostPutTable()
    {

        return Ok("Data from table");
    }

    [HttpGet]
    public IActionResult GetSearch(String stringSearch)
    {

        return Ok("Data from table");
    }
    

}