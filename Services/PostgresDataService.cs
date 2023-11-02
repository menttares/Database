using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
namespace Database.Services;
public class PostgresDataService
{
    private readonly string _connectionString;

    public PostgresDataService(string connectionString)
    {
        _connectionString = connectionString;
    }


    /// <summary>
    /// Возвращяет все названия колонок таблицы
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <returns>IEnumerable<String> названия колонок, иначе null</returns>
    public IEnumerable<String>? ColumsTable(String tableName)
    {
        try
        {
            if (tableName is null)
                throw new NullReferenceException("Null в параметре tableName не допустим");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                List<String> result = new();
                // SQL-запрос для получения столбцов таблицы
                string sql = "SELECT column_name FROM information_schema.columns WHERE table_name = @tableName ORDER BY ordinal_position;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tableName", tableName);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader.GetString(0);
                            result.Add(columnName);
                        }
                    }
                }

                return result;
            }
        }
        catch (System.Exception ex)
        {
            
            return null;
        }
    }

    public DataTable GetTable(string name)
    {

        return ExecuteQuery($"SELECT * FROM {name}");
    }

    public DataTable ExecuteQuery(string sql)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}
