using System.Text.Json;
using CSharpFunctionalExtensions;
using ImageSharing.Contracts;
using ImageSharing.Search.Domain.Interfaces;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.SharedKernel.Data.Storage;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Search.Infra.Repositories;

public sealed class SearchRepository(IStorageService storageService) : ISearchRepository
{
    public  async Task<Result> AddAsync(UserCreatedEvent user)
    {
        var searchUser = new SearchUser
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            Email = user.Email
        };
        
        var bytes = JsonSerializer.SerializeToUtf8Bytes(searchUser, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var stream = new MemoryStream(bytes);
        return await storageService.StoreFileAsync(stream, $"{user.Id}.json");
    }

    public async Task<Result> UpdateAsync(UpdatedUserEvent user)
    {
        var searchUser = new SearchUser
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            Email = user.Email,
            AvatarUrl = user.AvatarPath
        };
        
        var bytes = JsonSerializer.SerializeToUtf8Bytes(searchUser, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var stream = new MemoryStream(bytes);
        return await storageService.StoreFileAsync(stream, $"{user.Id}.json");
    }
}