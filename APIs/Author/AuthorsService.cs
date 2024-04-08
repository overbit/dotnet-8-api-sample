using MyService.Infrastructure;

namespace MyService.APIs;

public class AuthorsService : AuthorsServiceBase
{
    public AuthorsService(MyServiceContext context)
        : base(context) { }
}
