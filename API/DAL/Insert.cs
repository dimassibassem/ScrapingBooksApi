using System.Data.SqlClient;
using API.Models;


namespace API.DAL;

public class Insert
{
    public static void InsertBook(Book book)
    {
        var connection = DBConnection.GetConnection();
        //open connection
        connection.Open();

        try
        {
            SqlCommand command =
                new SqlCommand(
                    "INSERT INTO books (title, author, isbn, publisher, date, subject, type, cover)" +
                    "  VALUES (@title, @author, @isbn, @publisher, @date, @subject, @type, @cover)",
                    connection);
            if (book.Title != null) command.Parameters.AddWithValue("@title", book.Title);
            if (book.Author != null) command.Parameters.AddWithValue("@author", book.Author);
            if (book.ISBN != null) command.Parameters.AddWithValue("@isbn", book.ISBN);
            if (book.Publisher != null) command.Parameters.AddWithValue("@publisher", book.Publisher);
            if (book.Date != null) command.Parameters.AddWithValue("@date", book.Date);
            if (book.Subject != null) command.Parameters.AddWithValue("@subject", book.Subject);
            if (book.Type != null) command.Parameters.AddWithValue("@type", book.Type);
            if (book.Cover != null) command.Parameters.AddWithValue("@cover", book.Cover);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            connection.Close();
            Console.WriteLine(e);
        }
    }
}