using Microsoft.IO;

namespace ImageSharing.Storage.Azure.Abstractions;

public static class RecyclableMemoryStream
    {
        public static RecyclableMemoryStreamManager Manager { get; } = new()
        {

        };
    }
