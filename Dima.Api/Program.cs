using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.core.Handlers;
using Dima.core.Models;
using Dima.core.Requests.Categories;
using Dima.core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
    x =>
    {
        x.UseSqlServer(cnnStr);
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>{
    x.CustomSchemaIds(n => n.FullName);
});

builder
    .Services
    .AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost(
    pattern: "/v1/categories",
    handler: (
        CreateCategoryRequest request,
        ICategoryHandler handler)
    => handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.Run();