using GraphQL;
using GraphQL.Conventions.Relay;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using MyService.APIs.Author.Dtos;
using MyService.APIs.Todo.Dtos;
using MyService.APIs.Workspace.Dtos;

namespace MyService.APIs.Graphql;

[ImplementViewer(OperationType.Query)]
public class GqlQuery : ObjectGraphType<object>
{
    public GqlQuery()
    {
        Name = "query";

        // Leveraging the AutoRegisteringObjectGraphType<T> to automatically register the fields withouth having to manually define them
        Field<ListGraphType<AutoRegisteringObjectGraphType<AuthorDto>>>("authors")
            .Resolve()
            .WithScope()
            .WithService<IAuthorService>()
            .ResolveAsync(async (context, service) => await service.GetAuthors());

        Field<AutoRegisteringObjectGraphType<AuthorDto>>("author")
            .Resolve()
            .WithScope()
            .WithService<IAuthorService>()
            .ResolveAsync(async (context, service) => await service.GetAuthor(context.GetArgument<int>("id")));

        Field<ListGraphType<AutoRegisteringObjectGraphType<TodoItemDto>>>("todoItems")
            .Resolve()
            .WithScope()
            .WithService<ITodoItemsService>()
            .ResolveAsync(async (context, service) => await service.GetTodoItems());

        Field<AutoRegisteringObjectGraphType<TodoItemDto>>("todoItem")
            .Resolve()
            .WithScope()
            .WithService<ITodoItemsService>()
            .ResolveAsync(async (context, service) => await service.GetTodoItem(context.GetArgument<int>("id")));

        Field<ListGraphType<AutoRegisteringObjectGraphType<WorkspaceDto>>>("workspaces")
            .Resolve()
            .WithScope()
            .WithService<IWorkspacesService>()
            .ResolveAsync(async (context, service) => await service.GetWorkspaces());

        Field<AutoRegisteringObjectGraphType<WorkspaceDto>>("workspace")
            .Resolve()
            .WithScope()
            .WithService<IWorkspacesService>()
            .ResolveAsync(async (context, service) => await service.GetWorkspace(context.GetArgument<int>("id")));

    }
}