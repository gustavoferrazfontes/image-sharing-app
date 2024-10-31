using CSharpFunctionalExtensions;
using MediatR;

namespace ImageSharing.Auth.Domain.Commands;

public class SetUserAvatarCommand(Guid userId, string avatar64Base, string imageExtension) : IRequest<Result>
{
    public string Avatar { get; private set; } = avatar64Base;
    public string ImageExtension { get;private  set; } = imageExtension;
    public Guid UserId { get; private set; } =  userId;
}