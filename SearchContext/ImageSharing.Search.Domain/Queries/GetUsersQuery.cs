using CSharpFunctionalExtensions;
using ImageSharing.SharedKernel.Model;
using MediatR;

namespace ImageSharing.Search.Domain.Queries;

public sealed class GetUsersQuery(int pageSize, string? lastResultId) : IRequest<Result<PaginatedResult<GetUsersQueryResponse>>>
{
    public int PageSize { get; } = pageSize;
    public string? LastResultId { get; } = lastResultId;
}

public record GetUsersQueryResponse
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
}