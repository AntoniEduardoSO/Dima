using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.core.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Dima.Api.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddSwaggerGen(x =>{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
    
builder.Services.AddAuthorization();


builder.Services.AddDbContext<AppDbContext>(
    x =>
    {
        x.UseSqlServer(cnnStr);
    });


builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();



builder
    .Services
    .AddTransient<ICategoryHandler, CategoryHandler>()
    .AddTransient<ITransactionHandler,TransactionHandler>();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();



app.MapGet("/", () => new {message = "OK"});
app.MapEndpoints();
app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/logout", handler: async (
        SignInManager<User> signInManager) =>
        
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .RequireAuthorization();


app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", handler:  (
        ClaimsPrincipal user) =>
        
    {
        if(user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
        .Select(c => new 
        {
          c.Issuer,
          c.OriginalIssuer,
          c.Type,
          c.Value,
          c.ValueType
        });

        return TypedResults.Json(roles);
    })
    .RequireAuthorization();

app.Run();