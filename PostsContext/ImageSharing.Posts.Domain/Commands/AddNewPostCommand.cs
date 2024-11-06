using CSharpFunctionalExtensions;
using MediatR;

namespace ImageSharing.Posts.Domain.Commands;

public class AddNewPostCommand:IRequest<Result>
{
    public AddNewPostCommand(Guid userId,string? subtitle, string[]? tags)
    {
        UserId = userId;
        Subtitle = subtitle;
        Tags = tags;
    }

    public string[]? Tags { get;  }
    public string? Subtitle { get;  }
    public Guid UserId { get;  }
}