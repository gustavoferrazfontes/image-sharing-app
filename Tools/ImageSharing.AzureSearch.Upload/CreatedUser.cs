using Azure.Search.Documents.Indexes;

public class CreatedUser
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string? UserId { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true)]
    public string? UserName { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true)]
    public string? Email { get; set; }

    [SimpleField()]
    public string? AvatarUrl { get; set; }
}