using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using Database.Models;
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

    public DataTable AddOrder(OrderModel order)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand("add_order_procedure", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("p_date", NpgsqlDbType.Date) { Value = order.Date });
                cmd.Parameters.Add(new NpgsqlParameter("p_adres", NpgsqlDbType.Varchar) { Value = order.Adres });
                cmd.Parameters.Add(new NpgsqlParameter("p_customer", NpgsqlDbType.Integer) { Value = order.Customer });
                cmd.Parameters.Add(new NpgsqlParameter("p_furniture", NpgsqlDbType.Integer) { Value = order.Furniture });
                cmd.Parameters.Add(new NpgsqlParameter("p_count", NpgsqlDbType.Integer) { Value = order.Count });


                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }

    public DataTable SearchOrders(string searchTerm)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM search_orders(@searchTerm)", connection))
            {
                cmd.Parameters.Add(new NpgsqlParameter("searchTerm", NpgsqlDbType.Varchar)
                {
                    Value = searchTerm
                });

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }


    public DataTable FilterOrders(FilterOrderModel filter)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM filter_orders(@start_date, @end_date, @address, @customer_name, @furniture_name, @min_cost, @max_cost)", connection))
            {
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("start_date", NpgsqlDbType.Date)
                {
                    Value = filter.StartDate.HasValue ? filter.StartDate : (object)DBNull.Value
                });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", NpgsqlDbType.Date)
                {
                    Value = filter.EndDate.HasValue ? filter.EndDate : (object)DBNull.Value
                });
                cmd.Parameters.Add(new NpgsqlParameter("address", NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrEmpty(filter.Adres) ? (object)DBNull.Value : (object)filter.Adres
                });
                cmd.Parameters.Add(new NpgsqlParameter("customer_name", NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrEmpty(filter.Customer) ? (object)DBNull.Value : (object)filter.Customer
                });
                cmd.Parameters.Add(new NpgsqlParameter("furniture_name", NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrEmpty(filter.Furniture) ? (object)DBNull.Value : (object)filter.Furniture
                });
                cmd.Parameters.Add(new NpgsqlParameter("min_cost", NpgsqlDbType.Numeric)
                {
                    Value = filter.StartCost.HasValue ? filter.StartCost : (object)DBNull.Value
                });
                cmd.Parameters.Add(new NpgsqlParameter("max_cost", NpgsqlDbType.Numeric)
                {
                    Value = filter.EndCost.HasValue ? filter.EndCost : (object)DBNull.Value
                });

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }


    public int DeleteRow(string name, int Id)
    {

        String sql = $"DELETE FROM {name} WHERE Id = {Id};";
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
        }
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
