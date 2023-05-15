
namespace LibraryAPI.Models ;

public class Book{

    public int BookID ;

    public string Title {get;}
    public string PublisherName {get;}

    

        public Book(
            int bookID,
            string title,
            string publisherName
            )

    {
        //TODO: validations
        BookID = bookID ;
        Title = title ;
        PublisherName = publisherName ;
    }


    
}