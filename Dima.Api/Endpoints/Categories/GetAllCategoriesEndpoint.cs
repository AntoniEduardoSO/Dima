
using Dima.Api.Common.Api;
using Dima.core;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;
public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Categories: Get All")
    .WithSummary("Recupera todas as categorias")
    .WithDescription("Recupera todas as categorias")
    .WithOrder(5)
    .Produces<PagedResponse<List<Category>?>>();


    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromQuery]int PageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int PageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "test@balta.io",
            PageNumber = PageNumber,
            PageSize = PageSize,
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
