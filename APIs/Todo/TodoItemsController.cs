using Microsoft.AspNetCore.Mvc;

[ApiController]
public class TodoItemsController : TodoItemsControllerBase
{
    public TodoItemsController(ITodoItemsService service) : base(service) { }
}
