using System.Transactions;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet(
    pattern: "/v1/transactions",
    handler: () => new {message = "Hello"});

app.Run();


public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Type {get;set;}
    public decimal Amount {get;set;}
    public long CategoryId {get;set;}
    public string UserId {get;set;} = string.Empty;
}