

using LibraryAPI.Contracts.Book;
using LibraryAPI.Contracts.Borrower;
using LibraryAPI.Contracts.Loan;
using LibraryAPI.Models;
using Microsoft.Data.SqlClient;

namespace LibraryAPI.Services ;

public class DatabaseService : IDatabaseService

{

    // Databases Services for the Books API
    public static readonly string connectionString = @"Server=localhost;Database=LibraryManagement;User Id=SA;Password=password; TrustServerCertificate = true;";
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


    // Databases Services for the Borrowers API

    public void InsertBorrowerSQL(CreateBorrowerRequest request)
    {
        try
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "INSERT INTO borrower(BorrowerName, BorrowerAddress, BorrowerPhone) VALUES(@name, @address,  @phone)";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = request.BorrowerName;
                command.Parameters.Add("@address", System.Data.SqlDbType.NVarChar).Value = request.BorrowerAddress;
                command.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar).Value = request.BorrowerPhone;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public Borrower GetBorrowerSQL(int cardNo)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "SELECT [CardNo], [BorrowerName], [BorrowerAddress], [BorrowerPhone] FROM borrower WHERE [CardNo] = @cardNo ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@cardNo", System.Data.SqlDbType.Int).Value = cardNo;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                var borrower = new Borrower(
                    reader.GetInt32(0),
                    
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)
                );

                reader.Close();

                return borrower ;

            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());

            return null;
        }

    }


    public void UpdateBorrowerAddressSQL(int cardNo, string address)
    {

        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "UPDATE borrower SET BorrowerAddress = @address WHERE CardNo = @cardNo;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@address", System.Data.SqlDbType.NVarChar).Value = address;
                command.Parameters.Add("@cardNo", System.Data.SqlDbType.NVarChar).Value = cardNo;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void DeleteBorrowerSQL(int cardNo)
    {

        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "DELETE FROM borrower WHERE CardNo = @cardNo;";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add("@cardNo", System.Data.SqlDbType.NVarChar).Value = cardNo;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }

    }




    public void InsertLoanSQL(CreateLoanRequest request)
    {
        try
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "EXEC lib.BorrowBook @Title = @title, @CardNo = @cardNo, @BranchID = @branchID, @DueDate = @date ;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = request.Title;
                command.Parameters.Add("@cardNo", System.Data.SqlDbType.Int).Value = request.CardNo;
                command.Parameters.Add("@branchID", System.Data.SqlDbType.Int).Value = request.BranchID;
                // DateTime date;
                // bool success = DateTime.TryParse(request.DueDate, out date); 
                if(request.DueDate != null) 
                    {
                    DateTime date = DateTime.Parse(request.DueDate);
                    command.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;
                    }
                else{
                    command.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = DBNull.Value;

                }

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }



    public void ReturnBookSQL(string title, int cardNo, int branchID)

    {

        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sql = "EXEC lib.ReturnBook @Title = @title, @CardNo = @cardNo, @BranchID = @branchID ;";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = title;
                command.Parameters.Add("@cardNo", System.Data.SqlDbType.Int).Value = cardNo;
                command.Parameters.Add("@branchID", System.Data.SqlDbType.Int).Value = branchID;

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }


    }


     public List<Loan> GetLoansByCardNoSQL(int cardNo)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "SELECT * FROM loan where CardNo = @cardNo;";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@cardNo", System.Data.SqlDbType.Int).Value = cardNo;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                var loans = new List<Loan>();
                while (reader.Read())
                {
                    loans.Add(new Loan(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetDateTime(4),
                        reader.GetDateTime(5)
                    ))
                    ;


                }

                reader.Close();
                return loans;

            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }


}

