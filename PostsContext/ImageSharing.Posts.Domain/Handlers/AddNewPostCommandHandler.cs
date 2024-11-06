using CSharpFunctionalExtensions;
using ImageSharing.Posts.Domain.Commands;
using ImageSharing.Posts.Domain.Interfaces;
using ImageSharing.Posts.Domain.Models;
using ImageSharing.SharedKernel.Data.Storage;
using MediatR;

namespace ImageSharing.Posts.Domain.Handlers;

internal sealed class AddNewPostCommandHandler(IPostRepository postRepository)
    : IRequestHandler<AddNewPostCommand, Result>
{
    public Task<Result> Handle(AddNewPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(request.UserId,request.Subtitle,request.Tags);

        return postRepository.AddAsync(post);
    }
}