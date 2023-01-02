using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repository
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item item);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task RemoveAsync(Item item);
        Task UpdateAsync(Item item);
    }
}