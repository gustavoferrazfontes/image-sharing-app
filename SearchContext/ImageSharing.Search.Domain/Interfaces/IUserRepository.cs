using CSharpFunctionalExtensions;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Search.Domain.Interfaces;

public interface IUserRepository
{
    
   Task<Result<PaginatedResult<GetUsersQueryResponse>>> GetPaginatedAsync(int requestPageSize, string? requestLastResultId);
}