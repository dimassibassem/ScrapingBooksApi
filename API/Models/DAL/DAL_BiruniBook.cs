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

    public static BiruniBook GetBiruniBook(string title, string isbn, string edition)
    {
        using SqlConnection connection = DBConnection.GetConnection();
        BiruniBook book = new BiruniBook();
        try
        {
            connection.Open();
            string sql = " SELECT * FROM BiruniBook WHERE (ISBN = @isbn AND Title = @title AND Edition= @edition)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@isbn", isbn);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@edition", edition);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        book.Id = dataReader["Id"].ToString();
                        book.Title = dataReader["Title"].ToString();
                        book.Author = dataReader["Author"].ToString();
                        book.ISBN = dataReader["ISBN"].ToString();
                        book.Edition = dataReader["Edition"].ToString();
                        book.Collection = dataReader["Collection"].ToString();
                        book.Cover = dataReader["Cover"].ToString();
                    }
                }
            }

            connection.Close();
        }
        catch (Exception e)
        {
            connection.Close();
            Console.WriteLine(e);
            throw;
        }

        return book;
    }
}