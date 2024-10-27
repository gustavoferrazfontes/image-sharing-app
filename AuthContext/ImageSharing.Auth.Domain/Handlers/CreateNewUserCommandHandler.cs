using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.Contracts;
using MassTransit;
using MediatR;

namespace ImageSharing.Auth.Domain.Handlers;

internal sealed class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand>
{
    private readonly IUserEncryptService _userEncryptService;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateNewUserCommandHandler(IUserEncryptService userEncryptService, IPublishEndpoint publishEndpoint)
    {
        this._userEncryptService = userEncryptService;
        this._publishEndpoint = publishEndpoint;
    }
    public async Task Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var avatarPath = "";
        var salt = _userEncryptService.GenerateSalt();
        var hashedPassword = _userEncryptService.HashPassword(request.Password, salt);
        var base64Salt = Convert.ToBase64String(salt);

        var newUser = new User(request.UserName, request.Email, avatarPath, hashedPassword, salt);

        await _publishEndpoint.Publish(new CreatedUserEvent { Id = newUser.Id, Email = newUser.Email, UserName = newUser.UserName, Avatar = "" }, cancellationToken);

    }
}

