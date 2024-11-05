using ImageSharing.Search.Api.Models;
using ImageSharing.Search.Domain.Handlers.Consumers;
using ImageSharing.Search.Domain.Interfaces;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.Search.Infra.Repositories;
using ImageSharing.SharedKernel.Data.Storage;
using ImageSharing.Storage.Azure;
using ImageSharing.Storage.Azure.Abstractions;
using MassTransit;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },

            new string[]{}
        }
    });
});


builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<CreatedUserEventConsumer>();
    x.AddConsumer<UpdatedUserEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("Storage"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<BlobStorageSettings>>().Value);
builder.Services.AddScoped<IStorageService>(sp =>
{
    var settings = sp.GetRequiredService<BlobStorageSettings>();
    return new AzureStorageService(settings);
});

builder.Services.Configure<AzureSearchSettings>(builder.Configuration.GetSection("AzureSearch"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AzureSearchSettings>>().Value);
builder.Services.AddScoped<IUserRepository,UserRepository>(sp =>
{
    var setting = sp.GetRequiredService<AzureSearchSettings>();
    return new UserRepository(setting.SearchServiceUri,setting.SearchServiceApiKey);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

