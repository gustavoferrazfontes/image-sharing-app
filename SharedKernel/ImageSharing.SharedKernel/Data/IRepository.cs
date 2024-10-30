using System.Linq.Expressions;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.SharedKernel.Data;

public interface IRepository<T> where T : IEntity
{
   Task AddAsync(T entity);    
   Task<T?> GetByIdAsync(Guid id);
   Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T,bool>>? filters = null, string? includeProperties = null);
   Task UpdateAsync(T entity);
}