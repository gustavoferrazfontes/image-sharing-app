using System.Linq.Expressions;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.Auth.Infra.EF;
using Microsoft.EntityFrameworkCore;

namespace ImageSharing.Auth.Infra.Repositories;

public class UserRepository(AuthDbContext context) : IUserRepository
{
    public Task AddAsync(User entity)
    {
        context.Users.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<IEnumerable<User>?> GetAllAsync(Expression<Func<User, bool>>? filters = null, string? includeProperties = null)
    {
        IQueryable<User>? query = null;
        if (filters is not null)
        {
            query = context.Users.Where(filters);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            query?.Include(includeProperties);
        }
        return Task.FromResult(query?.AsEnumerable());
    }

    public Task UpdateAsync(User entity)
    {
        context.Users.Update(entity);
        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public Task<bool> IsEmailExistAsync(string email)
    {
        return context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }
}