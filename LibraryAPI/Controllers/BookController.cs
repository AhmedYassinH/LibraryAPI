


using LibraryAPI.Contracts.Book;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class BookController : ControllerBase {

    [HttpPost()]


    // TODO: Fix the database connection to happen only in the first time

    public IActionResult CreateBook(CreateBookRequest request)

    {

        //TODO: clean below
        // var book = new Book(
        //     request.Title,
        //     request.PublisherName
        // );

        InsertBookSQL(request);

// TODO: change the status code to created(string, object)
        return StatusCode(201);
    }

    private static void InsertBookSQL(CreateBookRequest request)
    {
        try
        {
            string connectionString = @"Server=DESKTOP-3VQDE5T\LIBRARYAI;Database=LibraryManagement;User Id=sa;Password=1234; TrustServerCertificate = true;";

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

    [HttpGet("{id:int}")]
    public IActionResult GetBook(int id)
    {
        var books = new List<Book>();
        GetBookSQL(id, books);
        if (books.Count != 0)
        {
            var res = JsonConvert.SerializeObject(books);
            Console.WriteLine(res);
            return Ok(res);
        }

        // TODO: add response if proplem occured
        return NotFound();
    }

    private static void GetBookSQL(int id, List<Book> books)
    {
        try
        {
            string connectionString = @"Server=DESKTOP-3VQDE5T\LIBRARYAI;Database=LibraryManagement;User Id=sa;Password=1234; TrustServerCertificate = true;";
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
}



