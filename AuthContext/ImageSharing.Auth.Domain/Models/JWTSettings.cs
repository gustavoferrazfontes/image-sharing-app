namespace ImageSharing.Auth.Domain.Models;

public record JWTSettings
{
    public string? ValidAudience { get; set; }
    public string? ValidIssuer { get; set; }
    public string? SecretKey { get; set; }
    public string? TokenValidityInMinutes { get; set; }
    public string? RefreshTokenValidityInMinutes { get; set; }
}
