using System.Data.SqlClient;
using API.Models.Entity;

namespace API.Models.DAL;

public static class DalBook
{
    public static void InsertBook(Book book)
    {
        var connection = DbConnection.GetConnection();
        try
        {
            connection.Open();
            var sqlRequest =
                "INSERT INTO books (title, author, isbn, publisher, date, subject, type, cover) VALUES (@title, @author, @isbn, @publisher, @date, @subject, @type, @cover)";
            SqlCommand command =
                new SqlCommand(sqlRequest, connection);

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

        connection.Close();
    }

    public static List<Book> GetBooks()
    {
        {
            List<Book> lstBook = new List<Book>();
            using SqlConnection connection = DbConnection.GetConnection();
            connection.Open();
            string sql = " SELECT * FROM Books";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var b = new Book
                        {
                            Id = dataReader["Id"].ToString(),
                            Title = dataReader["Title"].ToString(),
                            Author = dataReader["Author"].ToString(),
                            Date = dataReader["Date"].ToString(),
                            ISBN = dataReader["ISBN"].ToString(),
                            Publisher = dataReader["Publisher"].ToString(),
                            Subject = dataReader["Subject"].ToString(),
                            Cover = dataReader["Cover"].ToString(),
                            Type = dataReader["Type"].ToString()
                        };

                        lstBook.Add(b);
                    }
                }
            }

            connection.Close();

            return lstBook;
        }
    }

    public static Book GetBook(string isbn, string title)
    {
        using SqlConnection connection = DbConnection.GetConnection();
        Book book = new Book();
        try
        {
            connection.Open();
            string sql = " SELECT * FROM Books WHERE ISBN = @isbn AND Title = @title";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@isbn", isbn);
                command.Parameters.AddWithValue("@title", title);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        book.Id = dataReader["Id"].ToString();
                        book.Title = dataReader["Title"].ToString();
                        book.Author = dataReader["Author"].ToString();
                        book.Date = dataReader["Date"].ToString();
                        book.ISBN = dataReader["ISBN"].ToString();
                        book.Publisher = dataReader["Publisher"].ToString();
                        book.Subject = dataReader["Subject"].ToString();
                        book.Cover = dataReader["Cover"].ToString();
                        book.Type = dataReader["Type"].ToString();
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