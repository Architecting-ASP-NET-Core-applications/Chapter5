namespace Chapter5Routes.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpDelete]
    public IActionResult Handle404(string? path)
    {
        // Restituisce un messaggio di errore personalizzato
        return NotFound(new
        {
            Message = "The requested API endpoint does not exist.",
            Path = path,
            Timestamp = DateTime.UtcNow
        });
    }
}

