using System.Data.SqlClient;

namespace API.Models.DAL;

public class DALBiruniBook
{
    public static void InsertBook(BiruniBook book)
    {
        var connection = DBConnection.GetConnection();
        try
        {
            connection.Open();
            SqlCommand command =
                new SqlCommand(
                    "INSERT INTO BiruniBook (title, author, isbn,edition,collection, cover)" +
                    "  VALUES (@title, @author, @isbn,@edition,@collection, @cover)", connection);
            if (book.Title != null) command.Parameters.AddWithValue("@title", book.Title);
            if (book.Author != null) command.Parameters.AddWithValue("@author", book.Author);
            if (book.ISBN != null) command.Parameters.AddWithValue("@isbn", book.ISBN);
            if (book.Edition != null) command.Parameters.AddWithValue("@edition", book.Edition);
            if (book.Edition != null) command.Parameters.AddWithValue("@collection", book.Collection);
            if (book.Cover != null) command.Parameters.AddWithValue("@cover", book.Cover);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            connection.Close();
            Console.WriteLine(e);
        }

        connection.Close();
    }
}