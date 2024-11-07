using CSharpFunctionalExtensions;
using ImageSharing.Posts.Domain.Commands;
using ImageSharing.Posts.Domain.Interfaces;
using ImageSharing.Posts.Domain.Models;
using ImageSharing.SharedKernel.Data;
using MediatR;

namespace ImageSharing.Posts.Domain.Handlers;

internal sealed class AddNewPostCommandHandler(IPostRepository postRepository,IUnitOfWork unitOfWork)
    : IRequestHandler<AddNewPostCommand, Result>
{
    public async Task<Result> Handle(AddNewPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(request.UserId,request.Subtitle,request.Tags);

         await postRepository.AddAsync(post);

         await unitOfWork.CommitAsync();
         
         return Result.Success();
    }
}