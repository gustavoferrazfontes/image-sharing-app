using ImageSharing.Auth.Domain.Models;

namespace ImageSharing.Auth.Domain.Interfaces;

public interface IUserEncryptService
{
    string HashPassword(string password, byte[] salt);
    byte[] GenerateSalt();
    bool IsMatch(string salt, string passwordHash, string enteredPassword);

}
