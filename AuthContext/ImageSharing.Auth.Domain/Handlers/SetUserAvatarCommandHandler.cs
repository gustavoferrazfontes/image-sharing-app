using CSharpFunctionalExtensions;
using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.Contracts;
using ImageSharing.SharedKernel.Data;
using ImageSharing.SharedKernel.Data.Storage;
using MassTransit;
using MediatR;

namespace ImageSharing.Auth.Domain.Handlers;

public sealed class SetUserAvatarCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IStorageService storageService,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<SetUserAvatarCommand, Result>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task<Result> Handle(SetUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if(user == null)
            return Result.Failure("User not found");

        if (!string.IsNullOrWhiteSpace(request.Avatar))
        {

            return await TrySaveAvatarIntoStorage(request, user)
                .Tap(async res =>
                {
                    user.SetAvatarPath(res.FileId);
                    await userRepository.UpdateAsync(user);
                    await unitOfWork.CommitAsync();
                    
                    await _publishEndpoint.Publish(
                        new UpdatedUserEvent
                        {
                            Id = user.Id, 
                            UserName = user.UserName, 
                            Email = user.Email, 
                            AvatarPath = user.AvatarPath
                        }, cancellationToken);
                })
                .MapError(err => "Failed to save avatar");
        }
        return Result.Failure("Avatar is empty");
    }

    private async Task<Result<StoreFileResult>> TrySaveAvatarIntoStorage(SetUserAvatarCommand request, User user)
    {
        var avatarStream = new MemoryStream(Convert.FromBase64String(request.Avatar));
        var avatarPath = $"avatar/{user.Id}/{user.Id}.{request.ImageExtension}";
        
        return await storageService.StoreFileAsync(avatarStream, avatarPath);
    }
}