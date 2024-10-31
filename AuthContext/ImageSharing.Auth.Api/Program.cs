using ImageSharing.Auth.Dependencies;
using ImageSharing.Auth.Infra.EF;
using ImageSharing.SharedKernel.Data.Storage;
using ImageSharing.Storage.Azure;
using ImageSharing.Storage.Azure.Abstractions;
using MassTransit;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "enter a valid jwt token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            new string[] { }
        }
    });
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.AutoDelete = true;
        cfg.Host("localhost", h =>
         {
             h.Username("guest");
             h.Password("guest");
         });
        
        cfg.ConfigureEndpoints(context);
    });
});


builder.Services.AddAuth(builder.Configuration);
builder.Services.AddDbContext<AuthDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("AuthContext"))
);

builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("Storage"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<BlobStorageSettings>>().Value);
builder.Services.AddScoped<IStorageService>(sp =>
{
    var settings = sp.GetRequiredService<BlobStorageSettings>();
    return new AzureStorageService(settings);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();