using ImageSharing.Posts.Domain.Models;
using ImageSharing.Posts.Infra.EF.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ImageSharing.Posts.Infra.EF;

internal class PostDbContext:DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options):base(options)
    {
        
    }
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostMap).Assembly);
     }
    
        public DbSet<Post> Posts { get; set; }  
}