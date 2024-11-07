using CSharpFunctionalExtensions;
using ImageSharing.Auth.Infra.EF;
using ImageSharing.SharedKernel.Data;

namespace ImageSharing.Auth.Infra.Repositories;

public class UnitOfWork(AuthDbContext context) : IUnitOfWork
{
    public async Task<Result> CommitAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
           return  Result.Failure("An error occurred while saving changes to the database.");
        }
    }
}