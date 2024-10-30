
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Auth.Domain.Models;

public class User:IEntity
{
    protected User(){}
    
    public User(string userName, string email, string avatarPath, string hashedPassword, string salt)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        AvatarPath = avatarPath;
        HashedPassword = hashedPassword;
        Base64Salt = salt;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public string UserName { get; private set; } 
    public string Email { get; private set; } 
    public string AvatarPath { get;private set; } 
    public string HashedPassword { get; private set; } 
    public string Base64Salt { get; private set; } 
    public bool IsActive { get; private  set; }
    public bool IsDeleted { get;private set; }
    public DateTime CreatedAt { get; private set; }
}
