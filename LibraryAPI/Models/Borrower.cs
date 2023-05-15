namespace LibraryAPI.Models ;

// CardNo,BorrowerName,BorrowerAddress,BorrowerPhone
public class Borrower{


    public int CardNo {get;}

    public string BorrowerName {get;}

    public string BorrowerAddress {get;}

    public string BorrowerPhone {get;}


    public Borrower(
        int cardNo, 
        string borrowerName, 
        string borrowerAddress, 
        string borrowerPhone
    ){
        
        CardNo = cardNo;
        BorrowerName = borrowerName;
        BorrowerAddress = borrowerAddress;
        BorrowerPhone = borrowerPhone;
    }



}