using System.Data.SqlClient;

namespace API.DAL;

public class DBConnection
{
    static SqlConnection Connection = new("Data Source=localhost;Initial Catalog=books;User ID=SA;Password=yourStrong(!)Password");


    public static SqlConnection GetConnection()
    {
        return Connection;
    }
    
}