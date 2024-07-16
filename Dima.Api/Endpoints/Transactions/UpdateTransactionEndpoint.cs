using Dima.Api.Common.Api;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;
using Dima.core.Responses;

namespace Dima.Api.Endpoints.Transactions;
public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapPut("/{id}", HandleAsync)
     .WithName("Transactions: Update")
     .WithSummary("Atualiza uma transação")
     .WithDescription("Atualiza uma transação")
     .WithOrder(2)
     .Produces<Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        var result = await handler.UpdateAsync(request);

        request.UserId = "test@barra.io";
        request.Id = id;

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
