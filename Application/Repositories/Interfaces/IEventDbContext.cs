namespace Application.Repositories.Interfaces
{
    public interface IEventDbContext
    {
        Task BeginTransaction(CancellationToken ct);
        Task CommitTransaction(CancellationToken ct);
        Task RollBackTransaction(CancellationToken ct);
    }
}
