namespace LibraryAPI.Models ;


// LoanID,BookID,BranchID,CardNo,DateOut,DueDate

public class Loan {
    public int LoanID { get ;}

    public int BookID { get ; }

    public int BranchID { get ; }

    public int CardNo { get ; }

    public DateTime DateOut { get ; }

    public DateTime DueDate { get ; }

    public Loan(
        int loanID,
        int bookID,
        int branchID,
        int cardNo,
        DateTime dateOut,
        DateTime dueDate
    ) {
        LoanID = loanID;
        BookID = bookID;
        BranchID = branchID;
        CardNo = cardNo;
        DateOut = dateOut;
        DueDate = dueDate;
    }





}