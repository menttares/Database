using Microsoft.Data.SqlClient;
using Database.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Database
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Получаем строку подключения из файла конфигурации
            string? connectionString = builder.Configuration.GetConnectionString("ADO.NET");

            // Подключение сервиса PostgresDataService
            builder.Services.AddTransient<PostgresDataService>(_ => new PostgresDataService(connectionString));

            // Добавляем сервисы в контейнер.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Настраиваем конвейер HTTP-запросов.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
}
