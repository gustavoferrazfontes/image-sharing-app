using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using CSharpFunctionalExtensions;
using ImageSharing.Search.Domain.Interfaces;
using ImageSharing.Search.Domain.Queries;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Search.Infra.Repositories;

public class UserRepository:IUserRepository
{
    private readonly SearchClient _searchClient;

    public UserRepository(string azureSearchUri, string azureSearchKey)
    {
       _searchClient =new SearchClient(new Uri(azureSearchUri),"created-users-index",new AzureKeyCredential(azureSearchKey));
    }
    public async Task<Result<PaginatedResult<GetUsersQueryResponse>>> GetPaginatedAsync(int pageSize, string? lastResultId)
    {
        var searchOptions = new SearchOptions
        {
            IncludeTotalCount = true,
            Size = pageSize,
        };
        
        if (!string.IsNullOrWhiteSpace((lastResultId)))
            searchOptions.Filter = $"UserId gt '{lastResultId}'";

        var searchResult =await _searchClient.SearchAsync<SearchUser>("*", searchOptions);
        var query  = searchResult.Value.GetResults().Select(item => new GetUsersQueryResponse
        {
           
            UserId = item.Document.UserId,
            UserName = item.Document.UserName,
            Email = item.Document.Email,
            AvatarUrl = item.Document.AvatarUrl
        }).ToList();

        var result = new PaginatedResult<GetUsersQueryResponse>
        {
            Items = query,
            TotalCount = searchResult.Value.TotalCount
        };

        return Result.Success(result);





    }
}