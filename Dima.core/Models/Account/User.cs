namespace Dima.core.Models.Account;
public class User
{
    public string Email { get; set; } = string.Empty;
    public Dictionary<string, string> Claims {get;set;} = [];
}
