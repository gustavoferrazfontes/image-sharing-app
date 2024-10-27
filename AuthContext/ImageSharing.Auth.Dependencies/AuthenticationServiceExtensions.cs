using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using ImageSharing.Auth.Domain.Models;
using ImageSharing.Auth.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ImageSharing.Auth.Dependencies;

public static class AuthenticationServiceExtension 
{
    public static void AddAuth(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        serviceCollection.Configure<JWTSettings>(options => configurationManager.GetSection("JWT").Bind(options));
        var key = Encoding.ASCII.GetBytes(configurationManager["JWT:SecretKey"]);
        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidIssuer = configurationManager["JWT:ValidIssuer"],
                ValidAudience = configurationManager["JWT:ValidAudience"],
                ClockSkew = TimeSpan.Zero
            };

        });

        serviceCollection.AddScoped<TokenGenerator>();
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateNewUserCommand).Assembly));
        serviceCollection.AddScoped<IUserEncryptService,UserEncryptService>();
    }

}
