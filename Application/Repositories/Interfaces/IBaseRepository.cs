namespace EventPlanning.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateItemAsync(T item, CancellationToken ct = default);
        Task<T> GetItemByIdAsync(int itemId, CancellationToken ct = default);
        Task<T> UpdateItemByIdAsync(int itemId, T item, CancellationToken ct = default);
        Task DeleteItemByIdAsync(int itemId, CancellationToken ct = default);
        IQueryable<T> GetItems(CancellationToken ct = default);
    }
}
