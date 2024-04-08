using Microsoft.AspNetCore.Mvc;

namespace MyService.APIs;

[ApiController]
public class AuthorsController : AuthorsControllerBase
{
    public AuthorsController(IAuthorsService service)
        : base(service) { }
}
