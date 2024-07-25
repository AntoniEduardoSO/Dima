namespace Dima.core.Handlers;

using Dima.core.Requests.Account;
using Dima.core.Responses;
public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}
