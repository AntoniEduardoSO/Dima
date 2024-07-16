using Dima.Api.Data;
using Dima.core.Common.Extensions;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Transactions;
using Dima.core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;
public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceveidAt,
                Title = request.Title,
                Type = request.Type
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
        }
        catch
        {

            return new Response<Transaction?>(null, 500, "Não foi possível criar sua transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);


            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transação não encontrada.");
            }

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceveidAt;
            transaction.Title = request.Title;
            transaction.Type = request.Type;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch
        {

            return new Response<Transaction?>(null, 500, "Não foi possível recuperar a sua transãção");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);


            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transação não encontrada.");
            }

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();


            return new Response<Transaction?>(transaction);
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível recuperar a sua transãção");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);


            return transaction is null
                ? new Response<Transaction?>(null, 404, "Transação não encontrada.")
                : new Response<Transaction?>(transaction);
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível recuperar a sua transãção");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de inicio e termino");
        }

        try
        {
            var query = context
                .Transactions
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= request.StartDate &&
                    x.CreatedAt <= request.EndDate &&
                    x.UserId == request.UserId)
                .OrderBy(x => x.CreatedAt);
    
    
            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
    
            var count = await query.CountAsync();
    
            return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
        }


    }


}