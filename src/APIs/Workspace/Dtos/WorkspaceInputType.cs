using GraphQL.Types;

namespace MyService.APIs.Dtos;

public class WorkspaceInputType : InputObjectGraphType
{
    public WorkspaceInputType()
    {
        Name = "WorkspaceInput";
        Field<NonNullGraphType<IdGraphType>>("id");
        Field<StringGraphType>("name");
        Field<ListGraphType<NonNullGraphType<IdGraphType>>>("todoItemIds");
    }
}
