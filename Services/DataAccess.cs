using Database.Services.Interfeces;
using Microsoft.Data.SqlClient;
namespace Database.Services;

public class DataAccess : IDataAccess
{

    private SqlConnection connection {get; init;} 
    public DataAccess(string connectionString)
    {
        try
        {
            this.connection = new SqlConnection(connectionString);
        }
        catch (System.Exception ex)
        {
            
            connection.Close();
        }
    }

    ~DataAccess()
    {
        connection.Close();
    }
}