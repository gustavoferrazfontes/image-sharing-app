using ImageSharing.Posts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageSharing.Posts.Infra.EF;

internal class PostDbContext:DbContext
{
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Post).Assembly);
        }
    
        public DbSet<Post> Posts { get; set; }  
}