using LibraryAPI.Contracts.Borrower;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class BorrowersController : ControllerBase {

    private readonly IDatabaseService _databaseService ;

    public BorrowersController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }


    /// <summary>
    /// Creates a New Borrower
    /// </summary>

    /// <remarks>
    /// Sample request:
    ///
    ///     POST /borrowers
    ///     {
    ///         "BorrowerName": "John David",
    ///         "BorrowerAddress": "2212 Main Avenue",
    ///         "BorrowerPhone": "456-689-5522"
    ///     }
    /// </remarks>
    [HttpPost]
    public IActionResult CreateBorrower(CreateBorrowerRequest request)

    {

        _databaseService.InsertBorrowerSQL(request);

        // TODO: change the status code to created(string, object)
        return StatusCode(201);
    }





    /// <summary>
    /// Get a  Borrower
    /// </summary>

    /// <remarks>
    /// GET /borrowers/101
    /// </remarks>


    [HttpGet("{cardNo:int}")]
    
    public IActionResult GetBorrower(int cardNo)
    {
        
        var borrower = _databaseService.GetBorrowerSQL(cardNo);

        if (borrower != null){
            var res = JsonConvert.SerializeObject(borrower);
            return Ok(res);
        }

        return NotFound();
    }



    /// <summary>
    /// Update a Borrower's Address through cardNo
    /// </summary>

    /// <remarks>
    /// Sample request:
    /// PUT /borrowers/109/2212 Main Avenue
    /// </remarks>s
    [HttpPost("{cardNo:int}/{address}")]
    public IActionResult UpdateBorrowerAddress(int cardNo, string address)
    {
        _databaseService.UpdateBorrowerAddressSQL(cardNo, address);
        return Ok();
    }



    /// <summary>
    /// Update a Borrower from database using Card No
    /// </summary>

    /// <remarks>
    /// Sample request:
    /// DELETE /borrowers/109
    /// </remarks>



    [HttpDelete("{cardNo}")]

    public IActionResult DeleteBorrower(int cardNo)
    {

        _databaseService.DeleteBorrowerSQL(cardNo);
        return NoContent();
        
    }

    

    










}

