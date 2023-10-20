using Microsoft.Data.SqlClient;
using Database.Services;
using Database.Services.Interfeces;
namespace Database;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // получаем строку подключения из файла конфигурации
        string? connectionString = builder.Configuration.GetConnectionString("ADO.NET");
        
        // Подключение сервиса для база даннах
        builder.Services.AddTransient<IDataAccess, DataAccess>( provider => {
            return new DataAccess(connectionString);
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
