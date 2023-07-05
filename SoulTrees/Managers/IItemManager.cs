using SoulTrees.Models;

namespace SoulTrees.Managers
{
    public interface IItemManager
    {
        Task<bool> AddItem(Item item);
        Task<Item> GetItem(int id);
        Task<bool> UpdateItem(Item item);
        Task<List<Item>> GetItems(int page, int pageSize);
    }
}
