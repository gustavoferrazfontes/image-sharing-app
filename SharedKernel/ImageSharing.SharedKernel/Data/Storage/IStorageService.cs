using CSharpFunctionalExtensions;

namespace ImageSharing.SharedKernel.Data.Storage;

    public interface IStorageService
    {
        Task<Result<StoreFileResult>> StoreFileAsync(Stream stream, string filename);
        Task<Result<RetrieveFileResult>> RetriveFileAsync(string fileId);
        Task<Result> DeleteFileAsync(string fileId);
        Uri? GetblobSasUri(string fieldId, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false);
        bool TryGetblobSasUri(string fieldId, out string uri, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false);
        bool TryGetblobSasUri(string fieldId, out Uri? uri, TimeSpan? experationTime = null, string? contentType = null, bool? verifyBlob = false);
    }
