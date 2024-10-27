using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImageSharing.Auth.Domain.Models;

public class TokenGenerator
{
    private JWTSettings settings;

    public TokenGenerator(IOptions<JWTSettings> authTokenOption)
    {
        settings = authTokenOption.Value;
    }

    public string GenerateToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(settings.SecretKey);
        var tokenVaidityInMinutes = int.Parse(settings.TokenValidityInMinutes);

        SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(claims, key, tokenVaidityInMinutes);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateTokenDescriptor(List<Claim> claims, byte[] key, int tokenVaidityInMinutes)
    {
        return new SecurityTokenDescriptor
        {
            Issuer = settings.ValidIssuer,
            Audience = settings.ValidAudience,
            Expires = DateTime.UtcNow.AddMinutes(tokenVaidityInMinutes),
            Subject = new ClaimsIdentity(claims, "Bearer"),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
    }

}
