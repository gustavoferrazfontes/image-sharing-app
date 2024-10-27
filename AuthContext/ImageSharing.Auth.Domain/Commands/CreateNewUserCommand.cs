using MediatR;

namespace ImageSharing.Auth.Domain.Commands;

public sealed class CreateNewUserCommand : IRequest
{
    public CreateNewUserCommand(string userName, string email,string password, string avatar)
    {
        UserName = userName;
        Email = email;
        Password = password;
        Avatar = avatar;
    }

    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string Avatar { get; }
}
