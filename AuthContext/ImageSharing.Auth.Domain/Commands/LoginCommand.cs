using CSharpFunctionalExtensions;
using MediatR;

namespace ImageSharing.Auth.Domain.Commands;

public sealed class LoginCommand(string email, string password) : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}

public record LoginResponse
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}
