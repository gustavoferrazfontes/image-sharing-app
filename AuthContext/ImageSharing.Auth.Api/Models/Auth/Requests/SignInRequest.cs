namespace ImageSharing.Auth.Api.Models.Auth.Requests;

public record SignInRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
