
using LibraryAPI.Contracts.Book;
using LibraryAPI.Contracts.Borrower;
using LibraryAPI.Contracts.Loan;
using LibraryAPI.Models;

namespace LibraryAPI.Services;


public interface IDatabaseService
{

    // Databases Services for the Books API
    void InsertBookSQL(CreateBookRequest request);
    void GetBookSQL(int id, List<Book> books); 
    void UpdateBookSQL(int id, UpdateBookRequest request) ;
    public void DeleteBookSQL(int id) ;
    public void GetBooksByAuthorAndBranchSQL(string author, string branch, List<Book> books);
    public void GetBooksByKeywordSQL(string keyword, List<Book> books);


    // Databases Services for the Borrowers API

    public void InsertBorrowerSQL(CreateBorrowerRequest request);

    public Borrower GetBorrowerSQL(int cardNo);

    public void UpdateBorrowerAddressSQL(int cardNo, string address) ;

    public void DeleteBorrowerSQL(int cardNo) ;




    // Databases Services for the Loans API

    public void InsertLoanSQL(CreateLoanRequest request);

    public void ReturnBookSQL(string title, int cardNo, int branchID);


    public List<Loan> GetLoansByCardNoSQL(int cardNo);

}