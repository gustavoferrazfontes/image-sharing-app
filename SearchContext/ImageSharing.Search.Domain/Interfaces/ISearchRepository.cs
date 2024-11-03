using CSharpFunctionalExtensions;
using ImageSharing.Contracts;

namespace ImageSharing.Search.Domain.Interfaces;

public interface ISearchRepository
{
   Task<Result> AddAsync(UserCreatedEvent user);
   Task<Result> UpdateAsync(UpdatedUserEvent contextMessage);
}