using System.Data.SqlClient;

namespace API.Models.DAL;

public static class DbConnection
{
    static string DbConnnectionString = $"Data Source=localhost;Initial Catalog=books;User ID=SA;Password=yourStrong(!)Password";


    public static SqlConnection GetConnection()
    {
        return new SqlConnection(DbConnnectionString);
    }
    
}