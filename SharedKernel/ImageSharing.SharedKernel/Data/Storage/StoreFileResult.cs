namespace ImageSharing.SharedKernel.Data.Storage;

public record StoreFileResult(string FileId);

public record StorageFile( string FullName, Stream Stream,string Extension);