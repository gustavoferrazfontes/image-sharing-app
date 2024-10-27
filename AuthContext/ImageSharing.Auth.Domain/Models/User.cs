
namespace ImageSharing.Auth.Domain.Models;

public class User
{
    public User(string userName, string email, string avatarPath, string hashedPassword, byte[] salt)
    {
        UserName = userName;
        Email = email;
        AvatarPath = avatarPath;
        HashedPassword = hashedPassword;
        Salt = salt;
    }

    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string AvatarPath { get;private set; }
    public string HashedPassword { get; private set; }
    public byte[] Salt { get; private set; }
    public Guid Id { get; internal set; }
}
