using CSharpFunctionalExtensions;
using MediatR;

namespace ImageSharing.Auth.Domain.Commands;

public sealed class CreateNewUserCommand : IRequest<Result>
{
    public CreateNewUserCommand(string userName, string email,string password )
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
}
