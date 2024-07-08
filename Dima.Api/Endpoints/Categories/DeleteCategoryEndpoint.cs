using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
        .WithName("Categories: Delete")
        .WithSummary("Atualiza uma nova categoria")
        .WithDescription("Atualiza uma nova categoria")
        .WithOrder(3)
        .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = "teste@balta.io",
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
