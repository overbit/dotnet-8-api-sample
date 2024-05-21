using GraphQL;
using GraphQL.Conventions.Relay;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using MyService.APIs.Dtos;

namespace MyService.APIs.Graphql;

[ImplementViewer(OperationType.Mutation)]
public class GqlMutation : ObjectGraphType
{
    public GqlMutation()
    {
        Name = "mutation";

        Field<AutoRegisteringObjectGraphType<AuthorDto>>("createAuthor")
            .Arguments(new QueryArgument<AuthorInputType> { Name = "author" })
            .Resolve()
            .WithScope()
            .WithService<IAuthorsService>()
            .ResolveAsync(
                async (context, service) =>
                    await service.CreateAuthor(context.GetArgument<AuthorCreateInput>("author"))
            );
    }
}
