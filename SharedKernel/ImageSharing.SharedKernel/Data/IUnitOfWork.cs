namespace ImageSharing.SharedKernel.Data;

public interface IUnitOfWork
{
    Task CommitAsync();
}