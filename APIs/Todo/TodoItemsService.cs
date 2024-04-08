using MyService.Infrastructure;

namespace MyService.APIs;

public class TodoItemsService : TodoItemsServiceBase
{
    public TodoItemsService(MyServiceContext context)
        : base(context) { }
}
