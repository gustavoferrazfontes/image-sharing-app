using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using ImageSharing.Posts.Domain.Interfaces;
using ImageSharing.Posts.Domain.Models;
using ImageSharing.Posts.Infra.EF;

namespace ImageSharing.Posts.Infra.Repositories;

internal class PostRepository:IPostRepository
{
    private readonly PostDbContext _context;

    public PostRepository(PostDbContext context)
    {
        _context = context;
    }
    public Task AddAsync(Post entity)
    {
        _context.Posts.AddAsync(entity);
        return  Task.CompletedTask;
    }

    public Task<Post?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>?> GetAllAsync(Expression<Func<Post, bool>>? filters = null, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post entity)
    {
        throw new NotImplementedException();
    }
}