using CSharpFunctionalExtensions;
using ImageSharing.Contracts;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Search.Domain.Interfaces;

public interface ISearchRepository
{
   Task<Result> AddAsync(UserCreatedEvent user);
   Task<Result> UpdateAsync(UpdatedUserEvent contextMessage);
}