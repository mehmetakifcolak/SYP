using Serenity.Services;

namespace SYP.Customer;

public class CreateUserRequest : ServiceRequest
{
    public int CustomerId { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}

public class CreateUserResponse : ServiceResponse
{
    public int UserId { get; set; }
    public string Username { get; set; }
}
