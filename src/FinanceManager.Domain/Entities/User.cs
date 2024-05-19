using System.Text.Json.Serialization;

namespace FinanceManager.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = null!;
    
    [JsonIgnore]
    public string Password { get; set; } = null!;
    


}