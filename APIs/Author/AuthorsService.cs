using MyService.Infrastructure;

public class AuthorsService : AuthorsServiceBase
{
    public AuthorsService(MyServiceContext context) : base(context) { }
}
