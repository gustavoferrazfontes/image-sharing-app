using FluentAssertions;
using ImageSharing.Auth.Infra.Services;

namespace ImageSharing.Auth.Domain.Tests;

public class EncryptServiceTests
{
    private UserEncryptService sut;

    public EncryptServiceTests()
    {

        sut = new UserEncryptService();
    }
    [Fact]
    public void Should_Encrypt_a_password()
    {
        var password = "123456";
        var salt = sut.GenerateSalt();

        sut.HashPassword(password, salt).Should().NotBeNull();
    }

    [Theory]
    [InlineData("123456",true)]
    [InlineData("123457",false)]
    [InlineData("12ASD@254",false)]
    [InlineData("ASJFssjk!@#%#Ç@)",false)]
    public void Should_Match_password(string password,bool expectedResult)
    {
        var salt = "AMimrAT/jG9AVnDJw7pexA==";
        var passwordHash = "AMimrAT/jG9AVnDJw7pexHUufh5dou0kvmPqiZxqA91vaa+VPHLa5KR2MBxFZRLQ";

        sut.IsMatch(salt,passwordHash,password).Should().Be(expectedResult);
    }
}