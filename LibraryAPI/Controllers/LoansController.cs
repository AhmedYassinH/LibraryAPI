


using LibraryAPI.Contracts.Loan;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class LoansController : ControllerBase 
{

    private readonly IDatabaseService _databaseService ;

    public LoansController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }


    /// <summary>
    /// Create a loan/ Borrow a book
    /// </summary>

    /// <remarks>
    /// Sample request:
    ///     POST /Loans
    ///     {
    ///        "title": "it",
    ///        "cardNo": 100,
    ///        "branchID": 1,
    ///        "dueDate": NULL
    ///     }
    /// Note:
    ///     Due date is optional. If not provided, the default value is 15 days afterthe current date.
    /// </remarks>

    [HttpPut]
    public IActionResult CreateLoan(CreateLoanRequest request)
    {
        _databaseService.InsertLoanSQL(request);
        return StatusCode(201);



    }



    /// <summary>
    /// Delete a loan/ Return a book
    /// </summary>

    ///  <remarks>
    /// Sample request:
    ///     DELETE /Loans/It/100/1
    ///  </remarks>

    [HttpDelete("{title}/{cardNo}/{branchID}")]
    public IActionResult DeleteLoan(string title, int cardNo, int branchID)
    {
        _databaseService.ReturnBookSQL(title, cardNo, branchID);
        return NoContent();
    }




    /// <summary>
    /// Get all loans by cardNo
    /// </summary>

    /// <remarks>
    /// Sample request:
    ///     GET /Loans/100
    /// </remarks>

    [HttpGet("{cardNo:int}")]
    public IActionResult GetLoansByCardNo(int cardNo)
    {

        var loans = _databaseService.GetLoansByCardNoSQL(cardNo);

        if (loans != null)
        {
            var res = JsonConvert.SerializeObject(loans);
            return Ok(res);
        }

        return NotFound();


    }








}









