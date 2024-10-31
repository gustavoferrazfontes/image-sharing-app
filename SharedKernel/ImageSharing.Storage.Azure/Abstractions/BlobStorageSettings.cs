namespace ImageSharing.Storage.Azure.Abstractions;

public record class BlobStorageSettings
{
    public string? ConnectionString { get; set; } 
    public string? ContainerName { get; set; }
    public string? AccountKey { get; set; }
}

    
