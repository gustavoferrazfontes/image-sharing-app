using System.Globalization;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using CSharpFunctionalExtensions;
using ImageSharing.SharedKernel.Data.Storage;
using ImageSharing.Storage.Azure.Abstractions;

namespace ImageSharing.Storage.Azure;

public sealed class AzureStorageService : IStorageService
{
    private readonly BlobStorageSettings _settings;
    private readonly BlobContainerClient _containerClient;
    public AzureStorageService(BlobStorageSettings settings)
    {
        _settings = settings;

        _containerClient = new BlobContainerClient(_settings.ConnectionString, _settings.ContainerName,
            new BlobClientOptions()
            {
                Diagnostics =
                {
                    IsTelemetryEnabled = true,
                    IsDistributedTracingEnabled = true,
                },
            });
    }

    public async Task<Result> DeleteFileAsync(string fileId)
    {
        var blobClient = _containerClient.GetBlobClient(fileId);
        await blobClient.DeleteIfExistsAsync();

        return Result.Success();
    }

    public async Task<Result<RetrieveFileResult>> RetriveFileAsync(string fileId)
    {
        var blobClient = _containerClient.GetBlobClient(fileId);
        if (await blobClient.ExistsAsync())
        {
            var result = await blobClient.DownloadAsync().ConfigureAwait(false);

            using var resultValue = result.Value;
            var ms = RecyclableMemoryStream.Manager.GetStream("AzureBlobStorage");
            await resultValue.Content.CopyToAsync(ms).ConfigureAwait(false);
            ms.Seek(0, SeekOrigin.Begin);

            return Result.Success(new RetrieveFileResult(fileId, ms, fileId));
        }
        return Result.Success(new RetrieveFileResult(fileId, Stream.Null, fileId));
    }

    public async Task<Result<StoreFileResult>> StoreFileAsync(Stream stream, string filename)
    {
        try
        {
            var blobClient = _containerClient.GetBlobClient(filename);
            _ = await blobClient.UploadAsync(stream, overwrite: true);
            return Result.Success(new StoreFileResult(filename));
        }
        catch (Exception ex)
        {
            return Result.Failure<StoreFileResult>(ex.Message);
        }
    }

    public async Task<Result<List<StoreFileResult>>> StoreFilesAsync(List<StorageFile> stream)
    {
        try
        {
            List<StoreFileResult> result = new List<StoreFileResult>();
            foreach (var item in stream)
            {
                string fileName = item.FullName;
                var blobClient = _containerClient.GetBlobClient(fileName);
                _ = await blobClient.UploadAsync(item.Stream, overwrite: true);
                result.Add(new StoreFileResult(fileName));
            }
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<StoreFileResult>>(ex.Message);
        }
    }

    public Uri? GetblobSasUri(string fieldId, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false)
    {
        var blobClient = _containerClient.GetBlobClient(fieldId);

        if (verifyBlob != null && verifyBlob.Value)
            if (!blobClient.Exists())
                return null;

        // Set the expiration time for the SAS token
        experationTime = experationTime ?? new TimeSpan(0, 1, 0);

        var expirationTime = DateTime.Now.Add(experationTime.Value); // Set the expiration time as needed

        var expirationTimeUtc = expirationTime.ToUniversalTime();

        // Define the permissions for the SAS token
        var blobSasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTime.UtcNow,
            ExpiresOn = expirationTimeUtc
        };
        if (!string.IsNullOrWhiteSpace(contentType))
            blobSasBuilder.ContentType = contentType;

        blobSasBuilder.SetPermissions(BlobSasPermissions.Read); // Set the desired permissions

        // Generate the SAS token
        var sasToken = blobSasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(_containerClient.AccountName, _settings.AccountKey)).ToString();

        // Generate the blob URI with the SAS token

        var blobUriWithSas = new Uri(blobClient.Uri + "?" + sasToken);

        return blobUriWithSas;
    }

    public bool TryGetblobSasUri(string fieldId, out string uri, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false)
    {
        try
        {
            uri = GetblobSasUri(fieldId, experationTime, contentType, verifyBlob)?.ToString() ?? string.Empty;

            return true;
        }
        catch (Exception)
        {
            uri = string.Empty;

            return false;
        }
    }

    public bool TryGetblobSasUri(string fieldId, out Uri? uri, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false)
    {
        try
        {
            uri = GetblobSasUri(fieldId, experationTime, contentType, verifyBlob);

            return true;
        }
        catch (Exception)
        {
            uri = null;

            return false;
        }
    }
}