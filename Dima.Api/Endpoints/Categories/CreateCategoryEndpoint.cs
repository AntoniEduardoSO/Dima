using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Categories;
public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create")
        .WithSummary("Cria uma nova categoria")
        .WithDescription("Cria uma nova categoria")
        .WithOrder(1)
        .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
    {
        var result = await handler.CreateAsync(request);

        request.UserId = "test@barra.io";

        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
