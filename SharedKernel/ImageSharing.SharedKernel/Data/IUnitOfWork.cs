using CSharpFunctionalExtensions;

namespace ImageSharing.SharedKernel.Data;

public interface IUnitOfWork
{
    Task<Result> CommitAsync();
}