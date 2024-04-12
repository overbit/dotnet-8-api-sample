using Microsoft.AspNetCore.Mvc;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TodoItemFindMany : FindManyInput<TodoItem, TodoItemWhereInput> { }
