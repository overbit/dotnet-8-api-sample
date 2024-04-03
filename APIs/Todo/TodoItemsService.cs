using MyService.Infrastructure;


public class TodoItemsService : TodoItemsServiceBase
{
    public TodoItemsService(MyServiceContext context) : base(context) { }
}
