namespace ImageSharing.Auth.Api.Models.Auth.Requests;

public class SetAvatarRequest
{
    public string? Avatar64Base { get; set; }
    public string? ImageExtension { get; set; }
}