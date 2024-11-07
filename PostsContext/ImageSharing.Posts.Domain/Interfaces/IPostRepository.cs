using CSharpFunctionalExtensions;
using ImageSharing.Posts.Domain.Models;
using ImageSharing.SharedKernel.Data;

namespace ImageSharing.Posts.Domain.Interfaces;

internal interface IPostRepository:IRepository<Post>
{
}