
/* =================== STORED PROCEDURE QUERY QUESTIONS =================================== */


/* #1- Adding new borrower */


CREATE PROC lib.addBorrower
(@name VARCHAR(100) @address VARCHAR(200) @phone VARCHAR(50))
AS

INSERT INTO borrower (BorrowerName, BorrowerAddress, BorrowerPhone)
VALUES (@name, @address, @phone) ;


GO

EXEC lib.addBorrower @name='ahmed' @address='emu' @phone= '025641144'





/* #2- Borrow a book Knowing "Title,BorrowerCardNumber, BranchID" */


CREATE PROC lib.BorrowBook
(@Title VARCHAR(100), @CardNo INT, @BranchID INT, @DueDate DATE )

AS
DECLARE @BookID INT
DECLARE @Left INT


IF @DueDate IS NULL 
BEGIN
	SET @DueDate = DATEADD(day, 15 ,GETDATE())
END;



SELECT @BookID=book.BookID 
	FROM book 
	WHERE book.Title = @Title
	


SELECT @Left = CopiesLeft 
FROM copies 
WHERE BookID = @BookID AND BranchID = @BranchID


IF @Left > 0 

	BEGIN TRY

		UPDATE copies
			SET CopiesLeft = @Left - 1
			WHERE BookID = @BookID AND BranchID = @BranchID

		INSERT INTO loan
			(BookID, BranchID, CardNo, DateOut, DueDate)
			VALUES (@BookID, @BranchID, @CardNo, GETDATE(), @DueDate )



	END TRY

	BEGIN CATCH
	  PRINT('Sorry, Error Occurred')
	END CATCH;



ELSE
	PRINT ('Sorry, No Copies Left to Borrow')


GO 


EXEC lib.BorrowBook @Title = 'It', @CardNo = 100, @BranchID = 1, @DueDate = NULL 





/* #3- Return a book to the library "Title,BorrowerCardNumber, BranchID" */

CREATE OR ALTER PROC lib.ReturnBook
(@Title VARCHAR(100), @CardNo INT, @BranchID INT )
AS
DECLARE @BookID INT

SELECT @BookID=book.BookID 
	FROM book 
	WHERE book.Title = @Title

BEGIN TRY

	DELETE FROM loan
	WHERE loan.LoanID IN  
	   (SELECT TOP 1 loan.LoanID 
	    FROM loan 
		WHERE loan.BookID = @BookID AND loan.BranchID = @BranchID AND loan.CardNo= @CardNo
	    ORDER BY loan.LoanID DESC); 
		 IF @@ROWCOUNT <>1
		 BEGIN
			RAISERROR ('An error occured',10,1)
			RETURN -1
		 END


	UPDATE copies
		SET CopiesLeft = CopiesLeft + 1
		WHERE BookID = @BookID AND BranchID = @BranchID


END TRY
BEGIN CATCH
  PRINT('Sorry, Error Occurred check if there is a loan in the first place or not')
END CATCH;


GO 

EXEC lib.ReturnBook @Title = 'It', @CardNo = 100, @BranchID = 1











/* #4- For each book authored by "AuthorName", retrieve the title and the number of copies owned by "BranchName".*/

CREATE OR ALTER PROC lib.BookbyAuthorandBranch
	(@BranchName varchar(50), @AuthorName varchar(50))
AS
	SELECT book.BookID, book.Title AS [Title], book.PublisherName 
		   FROM author
				INNER JOIN book  ON author.BookID = book.BookID
				INNER JOIN copies  ON author.BookID = copies.BookID
				INNER JOIN  branch  ON copies.BranchID = branch.BranchID
			WHERE branch.BranchName = @BranchName AND author.AuthorName = @AuthorName
GO	
EXEC lib.BookbyAuthorandBranch @BranchName = 'Central', @AuthorName = 'Stephen King'











/* #4- For each book authored by "AuthorName", retrieve the title and the number of copies owned by "BranchName".*/

CREATE OR ALTER PROC lib.BookbyAuthorandBranch
	(@BranchName varchar(50), @AuthorName varchar(50))
AS
	SELECT book.BookID, book.Title AS [Title], copies.CopiesOwned AS [Number of Copies]
		   FROM author
				INNER JOIN book  ON author.BookID = book.BookID
				INNER JOIN copies  ON author.BookID = copies.BookID
				INNER JOIN  branch  ON copies.BranchID = branch.BranchID
			WHERE branch.BranchName = @BranchName AND author.AuthorName = @AuthorName
GO	
EXEC lib.BookbyAuthorandBranch @BranchName = 'Central', @AuthorName = 'Stephen King'



/* #5- For each book published by "PublisherName", retrieve the title and the number of copies owned by "BranchName".*/

CREATE PROC lib.BookbyPublisherandBranch
	(@BranchName varchar(50), @PublisherName varchar(100))
AS
	SELECT book.Title AS [Title], copies.CopiesOwned AS [Number of Copies]
		   FROM publisher
				INNER JOIN book  ON publisher.PublisherName = book.PublisherName
				INNER JOIN copies  ON book.BookID = copies.BookID
				INNER JOIN  branch  ON copies.BranchID = branch.BranchID
			WHERE branch.BranchName = @BranchName AND publisher.PublisherName = @PublisherName
GO	

EXEC lib.BookbyAuthorandBranch @BranchName = 'Central', @PublisherName = 'Bloomsbury'





/* #6- How many copies of the book titled "Title" are owned by the library branch whose name is "BranchName"? */

CREATE PROC lib.bookCopiesAtBranch 
(@Title varchar(70) , @BranchName varchar(70) )
AS
SELECT copies.BranchID AS [Branch ID], branch.BranchName AS [Branch Name],
	   copies.CopiesOwned AS [Number of Copies],
	   book.Title AS [Book Title]
	   FROM copies
			INNER JOIN book ON copies.BookID = book.BookID
			INNER JOIN branch  ON copies.BranchID = branch.BranchID
	   WHERE book.Title = @Title AND branch.BranchName = @BranchName
GO

EXEC lib.bookCopiesAtBranch @Title = 'The Lost Tribe' , @BranchName = 'Sharpstown'







/* #7- How many copies of the book titled "The Lost Tribe" are owned by each library branch? */

CREATE PROC lib.bookCopiesAtAllBranches 
(@bookTitle varchar(70) = 'The Lost Tribe')
AS
SELECT copies.BranchID AS [Branch ID], branch.BranchName AS [Branch Name],
	   copies.CopiesOwned AS [Number of Copies],
	   book.Title AS [Book Title]
	   FROM copies 
			INNER JOIN book ON copies.BookID = book.BookID
			INNER JOIN branch ON copies.BranchID = branch.BranchID
	   WHERE book.Title = @bookTitle 
GO
EXEC lib.bookCopiesAtAllBranches










/* #8- For each book that is loaned out from the "BranchName" branch and whose DueDate is today (or other custom date), retrieve the book title, the borrower's name, and the borrower's address.  */

CREATE PROC lib.BorrowersInfo 
(@DueDate date = NULL, @LibraryBranchName varchar(50) )
AS

IF @DueDate IS NULL 
BEGIN
	SET @DueDate = GETDATE()
END;
 
SELECT book.Title [Book Name],
	   borrower.BorrowerName AS [Borrower Name], borrower.BorrowerAddress AS [Borrower Address],
	   loan.DateOut AS [Date Out], loan.DueDate [Due Date]
	   FROM loan
			INNER JOIN book ON loan.BookID = book.BookID
			INNER JOIN borrower  ON loan.CardNo = borrower.CardNo
			INNER JOIN branch ON loan.BranchID = branch.BranchID
		WHERE loan.DueDate = @DueDate AND branch.BranchName = @LibraryBranchName
GO

EXEC lib.BorrowersInfo @DueDate = '01/16/18', @LibraryBranchName= 'Sharpstown'







/* #9- For each library branch, retrieve the branch name and the total number of books loaned out from that branch.  */

CREATE PROC lib.TotalLoansPerBranch
AS
SELECT  branch.BranchName AS [Branch Name], COUNT (loan.BranchID) AS [Total Loans]
		FROM loan
			INNER JOIN  branch ON loan.BranchID = branch.BranchID
			GROUP BY BranchName

GO
EXEC lib.TotalLoansPerBranch







/* 10- Retrieve the names, addresses, and number of books checked out for all borrowers who have more than five books checked out. */

CREATE PROC lib.BooksLoanedOut
(@BooksLimit INT = 5)
AS
	SELECT borrower.BorrowerName AS [Borrower Name], borrower.BorrowerAddress AS [Borrower Address],
		   COUNT(borrower.BorrowerName) AS [Books Checked Out]
		   FROM loan
		   			INNER JOIN borrower ON loan.CardNo = borrower.CardNo
					GROUP BY borrower.BorrowerName, borrower.BorrowerAddress
		   HAVING COUNT(borrower.BorrowerName) >= @BooksCheckedOut
		   ORDER BY COUNT(borrower.BorrowerName) DESC
GO
EXEC lib.BooksLoanedOut



