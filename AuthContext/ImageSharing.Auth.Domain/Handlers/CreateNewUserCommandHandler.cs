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

internal sealed class CreateNewUserCommandHandler(
    IUserEncryptService userEncryptService,
    IPublishEndpoint publishEndpoint,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateNewUserCommand,Result>
{
    public async Task<Result> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        
        
        var salt = userEncryptService.GenerateSalt();
        var hashedPassword = userEncryptService.HashPassword(request.Password, salt);
        var base64Salt = Convert.ToBase64String(salt);

        var newUser = new User(request.UserName, request.Email, hashedPassword, base64Salt);
       if((await userRepository.IsEmailExistAsync(newUser.Email)))
            Result.Failure("Email already exist"); 
       
        await userRepository.AddAsync(newUser);
        await unitOfWork.CommitAsync();
        
        var userCreatedEvent = new UserCreatedEvent{Id= newUser.Id, UserName = newUser.UserName, Email = newUser.Email};
        await publishEndpoint.Publish(userCreatedEvent, cancellationToken);

        return Result.Success();
    }
}

