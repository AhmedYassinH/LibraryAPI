


using Microsoft.AspNetCore.Mvc;


[Route("/error")]
public class ErrorController:ControllerBase{

protected IActionResult Error()
{
 return Problem();
}
}