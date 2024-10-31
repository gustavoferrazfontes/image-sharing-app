using CSharpFunctionalExtensions;
using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.SharedKernel.Data;
using ImageSharing.SharedKernel.Data.Storage;
using MediatR;

namespace ImageSharing.Auth.Domain.Handlers;

public sealed class SetUserAvatarCommandHandler
    : IRequestHandler<SetUserAvatarCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public SetUserAvatarCommandHandler( IUnitOfWork unitOfWork,IUserRepository userRepository, IStorageService storageService)
    {
        _storageService = storageService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SetUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if(user == null)
            return Result.Failure("User not found");

        if (!string.IsNullOrWhiteSpace(request.Avatar))
        {

            return await TrySaveAvatarIntoStorage(request, user)
                .Tap(async res =>
                {
                    user.SetAvatarPath(res.FileId);
                    await _userRepository.UpdateAsync(user);

                    await _unitOfWork.CommitAsync();
                })
                .MapError(err => "Failed to save avatar");
            
        }

        return Result.Failure("Avatar is empty");
    }

    private async Task<Result<StoreFileResult>> TrySaveAvatarIntoStorage(SetUserAvatarCommand request, User user)
    {
        var avatarStream = new MemoryStream(Convert.FromBase64String(request.Avatar));
        var avatarPath = $"avatar/{user.Id}/{user.Id}.{request.ImageExtension}";
        
        return await _storageService.StoreFileAsync(avatarStream, avatarPath);
    }
}