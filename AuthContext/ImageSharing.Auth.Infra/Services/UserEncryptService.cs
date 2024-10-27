using ImageSharing.Auth.Domain.Interfaces;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;
namespace ImageSharing.Auth.Infra.Services;

internal class UserEncryptService : IUserEncryptService
{
    public string HashPassword(string password, byte[] salt)
    {

        var sha256 = SHA256.Create();

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        // Concatenate password and salt
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        // Hash the concatenated password and salt
        byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

        // Concatenate the salt and hashed password for storage
        byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
        Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
        Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

        return Convert.ToBase64String(hashedPasswordWithSalt);
    }

    public byte[] GenerateSalt()
    {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();

        byte[] salt = new byte[16];
        rng.GetBytes(salt);
        return salt;
    }

    public bool IsMatch(string salt, string passwordHash, string enteredPassword)
    {
        // Convert the stored salt and entered password to byte arrays
        byte[] storedSaltBytes = Convert.FromBase64String(salt);
        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

        // Concatenate entered password and stored salt
        byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
        Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
        Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

        // Hash the concatenated value
        string enteredPasswordHash = HashPassword(enteredPassword, storedSaltBytes);

        // Compare the entered password hash with the stored hash
        return enteredPasswordHash == passwordHash;

    }
}
