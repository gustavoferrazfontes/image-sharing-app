namespace ImageSharing.Search.Api.Models;

public record class AzureSearchSettings
{
    public string?  SearchServiceUri{ get; set; } 
    public string?  SearchServiceApiKey{ get; set; } 
}