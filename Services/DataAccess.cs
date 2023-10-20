using Database.Services.Interfeces;
using Microsoft.Data.SqlClient;
namespace Database.Services;

public class DataAccess : IDataAccess
{

    private SqlConnection connection {get; init;} 
    public DataAccess(string connectionString)
    {
        this.connection = new SqlConnection(connectionString);
    }



    ~DataAccess()
    {
        connection.Close();
    }
}