using MediatR;

namespace ImageSharing.Auth.Domain.Commands;

public class LoginCommand:IRequest<LoginResponse>
{
    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}

public record LoginResponse
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}
