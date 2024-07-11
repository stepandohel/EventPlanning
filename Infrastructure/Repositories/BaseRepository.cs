using Domain;
using Domain.Models.Base;
using EventPlanning.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPlanning.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateItemAsync(T item, CancellationToken ct = default)
        {
            try
            {
                var newItem = await _context.Set<T>().AddAsync(item);
                await _context.SaveChangesAsync();

                return newItem.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteItemByIdAsync(int itemId, CancellationToken ct = default)
        {
            try
            {
                var itemForDelete = await _context.Set<T>().AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(itemId));
                if (itemForDelete is not null)
                {
                    _context.Set<T>().Remove(itemForDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetItemByIdAsync(int itemId, CancellationToken ct = default)
        {
            try
            {
                var findedItem = await _context.Set<T>().AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(itemId));

                return findedItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> GetItems(CancellationToken ct = default)
        {
            try
            {
                var items = _context.Set<T>().AsNoTracking();

                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateItemByIdAsync(int itemId, T item, CancellationToken ct = default)
        {
            try
            {
                var itemForUpdate = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(itemId));
                if (itemForUpdate is null)
                {
                    throw new ArgumentException();                   
                }

                item.Id = itemForUpdate.Id;
                _context.Set<T>().Update(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
