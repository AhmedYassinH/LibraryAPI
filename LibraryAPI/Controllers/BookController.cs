


using LibraryAPI.Contracts.Book;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class BookController : ControllerBase {

    private readonly IDatabaseService _databaseService ;

    public BookController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }



    [HttpPost]
    public IActionResult CreateBook(CreateBookRequest request)

    {

        _databaseService.InsertBookSQL(request);

// TODO: change the status code to created(string, object)
        return StatusCode(201);
    }


    [HttpGet("{id:int}")]

    public IActionResult GetBook(int id)
    {
        var books = new List<Book>();
        _databaseService.GetBookSQL(id, books);
        if (books.Count != 0)
        {
            var res = JsonConvert.SerializeObject(books);
            return Ok(res);
        }

        // TODO: add response if proplem occured
        return NotFound();
    }

  
    [HttpPut("{id}")]

    public IActionResult UpdateBook(int id, UpdateBookRequest request)
    {
        _databaseService.UpdateBookSQL(id, request);

        // TODO: change to created at response + add response in case no record to update is found

        return Ok();
    }



    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
         _databaseService.DeleteBookSQL(id);

        return NoContent();
    }

 
}


