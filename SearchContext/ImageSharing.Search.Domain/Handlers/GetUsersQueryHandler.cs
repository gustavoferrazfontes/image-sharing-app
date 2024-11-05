using CSharpFunctionalExtensions;
using ImageSharing.Search.Domain.Interfaces;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.SharedKernel.Data.Storage;
using ImageSharing.SharedKernel.Model;
using MediatR;

namespace ImageSharing.Search.Domain.Handlers;

public sealed class GetUsersQueryHandler(IStorageService storageService, IUserRepository repository)
    : IRequestHandler<GetUsersQuery, Result<PaginatedResult<GetUsersQueryResponse>>>
{
    public Task<Result<PaginatedResult<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
         return repository.GetPaginatedAsync(request.PageSize, request.LastResultId)
            .Tap(item =>
            {
                item.Items?.ToList().ForEach(item =>
                {
                    var _ =  storageService.TryGetblobSasUri(item.UserId,  out string url,new TimeSpan(0, 5, 0));
                    item.AvatarUrl = url;
                });
            });
    }
}