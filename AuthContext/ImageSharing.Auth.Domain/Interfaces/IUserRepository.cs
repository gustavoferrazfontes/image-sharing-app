using ImageSharing.Auth.Domain.Models;
using ImageSharing.SharedKernel.Data;

namespace ImageSharing.Auth.Domain.Interfaces;

public interface IUserRepository:IRepository<User>
{
   Task<User> GetByEmailAsync(string email); 
   Task<bool> IsEmailExistAsync(string email);  
}