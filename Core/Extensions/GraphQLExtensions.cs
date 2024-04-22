using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using MyService.APIs.Graphql;

namespace MyService;

public static class GraphQLExtensions
{
    public static void RegisterGraphQL(this IServiceCollection services)
    {
        // Add graphql services to the container.
        services.AddSingleton<ISchema, GqlSchema>(services => new GqlSchema(
            new SelfActivatingServiceProvider(services)
        ));
        services.AddSingleton(typeof(AutoRegisteringInputObjectGraphType<>));
        services.AddSingleton(typeof(AutoRegisteringObjectGraphType<>));
        services.AddSingleton(typeof(EnumerationGraphType<>));

        services.AddGraphQL(b =>
            b.AddSystemTextJson()
                .AddDataLoader()
                .AddAutoSchema<GqlSchema>()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
        );
    }

    public static void MapGraphQLEndpoints(this WebApplication app)
    {
        app.MapGraphQL("/graphql").RequireCors("MyCorsPolicy");
        app.MapGraphQLPlayground("/graphql/ui");
        app.MapGraphQLVoyager();
    }
}
