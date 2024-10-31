namespace ImageSharing.SharedKernel.Data.Storage;

public record RetrieveFileResult(string FileId, Stream Stream, string FileName);