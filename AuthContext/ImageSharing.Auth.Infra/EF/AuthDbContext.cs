using ImageSharing.Auth.Domain.Models;
using ImageSharing.Auth.Infra.EF.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ImageSharing.Auth.Infra.EF;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
    }

    public DbSet<User> Users { get; set; }  
}