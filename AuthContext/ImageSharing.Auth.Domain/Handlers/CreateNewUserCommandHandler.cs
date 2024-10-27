using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.Contracts;
using MassTransit;
using MediatR;

namespace ImageSharing.Auth.Domain.Handlers;

internal sealed class CreateNewUserCommandHandler(
    IUserEncryptService userEncryptService,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateNewUserCommand>
{
    public async Task Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        
        var avatarPath = "";
        var salt = userEncryptService.GenerateSalt();
        var hashedPassword = userEncryptService.HashPassword(request.Password, salt);
        var base64Salt = Convert.ToBase64String(salt);

        var newUser = new User(request.UserName, request.Email, avatarPath, hashedPassword, salt);

        await publishEndpoint.Publish(new CreatedUserEvent { Id = newUser.Id, Email = newUser.Email, UserName = newUser.UserName, Avatar = "" }, cancellationToken);

    }
}

