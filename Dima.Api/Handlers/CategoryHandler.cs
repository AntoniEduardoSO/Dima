using Dima.Api.Data;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;
public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category>(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Falha ao criar a categoria");
        }
    }

    public Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}