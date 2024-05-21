namespace MyService.APIs.Graphql;

public class GqlSchema : GraphQL.Types.Schema
{
    public GqlSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<GqlQuery>()!;
        Mutation = serviceProvider.GetRequiredService<GqlMutation>()!;
    }
}
