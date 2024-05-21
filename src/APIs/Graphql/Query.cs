using GraphQL;
using GraphQL.Conventions.Relay;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using MyService.APIs.Dtos;

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
            .WithService<IAuthorsService>()
            .ResolveAsync(async (context, service) => await service.Authors(null));

        Field<AutoRegisteringObjectGraphType<AuthorDto>>("author")
            .Resolve()
            .WithScope()
            .WithService<IAuthorsService>()
            .ResolveAsync(
                async (context, service) =>
                    await service.Author(context.GetArgument<AuthorIdDto>("id"))
            );

        Field<ListGraphType<AutoRegisteringObjectGraphType<TodoItemDto>>>("todoItems")
            .Resolve()
            .WithScope()
            .WithService<ITodoItemsService>()
            .ResolveAsync(async (context, service) => await service.TodoItems(null));

        Field<AutoRegisteringObjectGraphType<TodoItemDto>>("todoItem")
            .Resolve()
            .WithScope()
            .WithService<ITodoItemsService>()
            .ResolveAsync(
                async (context, service) =>
                    await service.TodoItem(context.GetArgument<TodoItemIdDto>("id"))
            );

        Field<ListGraphType<AutoRegisteringObjectGraphType<WorkspaceDto>>>("workspaces")
            .Resolve()
            .WithScope()
            .WithService<IWorkspacesService>()
            .ResolveAsync(async (context, service) => await service.Workspaces(null));

        Field<AutoRegisteringObjectGraphType<WorkspaceDto>>("workspace")
            .Resolve()
            .WithScope()
            .WithService<IWorkspacesService>()
            .ResolveAsync(
                async (context, service) =>
                    await service.Workspace(context.GetArgument<WorkspaceIdDto>("id"))
            );
    }
}
