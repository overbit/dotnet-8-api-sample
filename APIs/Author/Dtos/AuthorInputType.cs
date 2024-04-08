using GraphQL.Types;

namespace MyService.APIs.Dtos;

public class AuthorInputType : InputObjectGraphType
{
    public AuthorInputType()
    {
        Name = "AuthorInput";
        Field<NonNullGraphType<IdGraphType>>("id");
        Field<StringGraphType>("name");
        Field<ListGraphType<NonNullGraphType<IdGraphType>>>("todoItemIds");
    }
}
