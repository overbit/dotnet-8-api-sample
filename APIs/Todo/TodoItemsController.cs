using Microsoft.AspNetCore.Mvc;

namespace MyService.APIs;

[ApiController]
public class TodoItemsController : TodoItemsControllerBase
{
    public TodoItemsController(ITodoItemsService service)
        : base(service) { }
}
