using ImageSharing.Auth.Infra.EF;
using ImageSharing.SharedKernel.Data;

namespace ImageSharing.Auth.Infra.Repositories;

public class UnitOfWork(AuthDbContext context) : IUnitOfWork
{
    public Task CommitAsync()
    {
        try
        {
            return context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}