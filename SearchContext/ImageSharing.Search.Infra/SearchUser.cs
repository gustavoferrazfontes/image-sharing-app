using Azure.Search.Documents.Indexes;
using ImageSharing.Search.Domain.Interfaces;

namespace ImageSharing.Search.Infra;

public class SearchUser
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string UserId { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true)]
    public string? UserName { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true)]
    public string? Email { get; set; }

    [SimpleField()]
    public string? AvatarUrl { get; set; }
}