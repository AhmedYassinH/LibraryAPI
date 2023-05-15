namespace LibraryAPI.Contracts.Loan;




// {
//     "Title": "It",
//     "CardNo": 101,
//     "BranchID": 1,
//     "DueDate": NULL
// }
public record CreateLoanRequest(

    string Title,
    int CardNo,
    int BranchID,
    string? DueDate

);



