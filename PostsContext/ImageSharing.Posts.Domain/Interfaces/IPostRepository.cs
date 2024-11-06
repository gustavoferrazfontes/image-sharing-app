using CSharpFunctionalExtensions;
using ImageSharing.Posts.Domain.Models;

namespace ImageSharing.Posts.Domain.Interfaces;

internal interface IPostRepository
{
   Task<Result> AddAsync(Post post); 
}