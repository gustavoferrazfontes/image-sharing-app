using CSharpFunctionalExtensions;
using ImageSharing.SharedKernel.Model;

namespace ImageSharing.Posts.Domain.Models;

internal class Post:IEntity
{
    protected Post(){}
    
    public Post(Guid userId,string? subtitle, string[]? tags)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Subtitle = subtitle;
        Tags = tags;
    }
    

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string? Subtitle { get; private set; }
    public string[]? Tags { get; private set; }
    
    public string ImagePath { get; private set; }
}