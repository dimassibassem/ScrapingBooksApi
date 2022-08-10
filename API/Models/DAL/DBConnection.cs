using System.Data.SqlClient;

namespace API.Models.DAL;

public static class DbConnection
{
    private const string DbConnectionString = "Data Source=localhost;Initial Catalog=books;User ID=SA;Password=yourStrong(!)Password";


    public static SqlConnection GetConnection()
    {
        return new SqlConnection(DbConnectionString);
    }
    
}