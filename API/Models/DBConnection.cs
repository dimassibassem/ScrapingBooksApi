using System.Data.SqlClient;

namespace API.Models;

public static class DbConnection
{
    private const string DbConnectionString = "Data Source=localhost;Initial Catalog=books;User ID=SA;Password=msSQL2022";


    public static SqlConnection GetConnection()
    {
        return new SqlConnection(DbConnectionString);
    }
    
}