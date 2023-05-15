namespace LibraryAPI.Contracts.Borrower;


public record CreateBorrowerRequest(
    string BorrowerName,
    string BorrowerAddress,
    string BorrowerPhone

);