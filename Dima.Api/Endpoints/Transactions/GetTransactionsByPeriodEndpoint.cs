using Dima.Api.Common.Api;
using Dima.core;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;
using Dima.core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;
public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Transactions: Get All")
    .WithSummary("Recupera todas as transações")
    .WithDescription("Recupera todas as transações")
    .WithOrder(5)
    .Produces<PagedResponse<List<Transaction>?>>();


    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        [FromQuery]DateTime? startDate = null,
        [FromQuery]DateTime? endDate = null,
        [FromQuery]int PageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int PageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionByPeriodRequest
        {
            UserId = "test@balta.io",
            PageNumber = PageNumber,
            PageSize = PageSize,
            StartDate = startDate,
            EndDate = endDate 
        };

        var result = await handler.GetByPeriodAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}