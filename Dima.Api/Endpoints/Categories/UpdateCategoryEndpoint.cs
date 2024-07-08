using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Categories;
public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
        .WithName("Categories: Update")
        .WithSummary("Atualiza uma nova categoria")
        .WithDescription("Atualiza uma nova categoria")
        .WithOrder(2)
        .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        var result = await handler.UpdateAsync(request);

        request.UserId = "test@barra.io";
        request.Id = id;

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}