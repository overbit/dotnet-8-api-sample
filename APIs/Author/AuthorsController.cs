using Microsoft.AspNetCore.Mvc;

[ApiController]
public class AuthorsController : AuthorsControllerBase
{
    public AuthorsController(IAuthorsService service) : base(service) { }
}
