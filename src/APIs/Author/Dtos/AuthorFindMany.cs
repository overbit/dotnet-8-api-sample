using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Common;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class AuthorFindMany : FindManyInput<Author, AuthorWhereInput> { }
