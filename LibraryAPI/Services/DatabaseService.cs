

using LibraryAPI.Contracts.Book;
using LibraryAPI.Models;
using Microsoft.Data.SqlClient;

namespace LibraryAPI.Services ;

public class DatabaseService : IDatabaseService

{
    public static readonly string connectionString = @"Server=DESKTOP-3VQDE5T\LIBRARYAI;Database=LibraryManagement;User Id=sa;Password=1234; TrustServerCertificate = true;";
    public void InsertBookSQL(CreateBookRequest request)
    {

        try
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "INSERT INTO book(Title, PublisherName) VALUES (@title,@publisher) ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = request.Title;
                command.Parameters.Add("@publisher", System.Data.SqlDbType.NVarChar).Value = request.PublisherName;

                connection.Open();

                command.ExecuteReader();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void GetBookSQL(int id, List<Book> books)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "SELECT [BookID], [Title],[PublisherName] FROM book WHERE [BookID] = @id ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    ));


                }

                reader.Close();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void UpdateBookSQL(int id, UpdateBookRequest request)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "UPDATE book SET Title = @title WHERE BookID = @id ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = request.Title;
                command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = id;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }



    public void DeleteBookSQL(int id)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "DELETE FROM book WHERE BookID = @id ;";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = id;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void GetBooksByAuthorAndBranchSQL(string author, string branch, List<Book> books)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "EXEC lib.BookbyAuthorandBranch @BranchName = @branch, @AuthorName =  @author  ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@author", System.Data.SqlDbType.Text).Value = author;
                command.Parameters.Add("@branch", System.Data.SqlDbType.Text).Value = branch ;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    ));


                }

                reader.Close();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void GetBooksByKeywordSQL(string keyword, List<Book> books)
    {

        keyword = '%'+ keyword + '%';
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "SELECT BookID, Title, PublisherName FROM book WHERE Title LIKE @keyword  ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@keyword", System.Data.SqlDbType.Text).Value = keyword;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    ));


                }

                reader.Close();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }


}

