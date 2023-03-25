
using LibraryAPI.Contracts.Book;
using LibraryAPI.Models;

namespace LibraryAPI.Services;


public interface IDatabaseService
{
    void InsertBookSQL(CreateBookRequest request);

    void GetBookSQL(int id, List<Book> books); 

    void UpdateBookSQL(int id, UpdateBookRequest request) ;

    public void DeleteBookSQL(int id) ;
}