using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public interface IItemRepository
    {
        bool AddItem(Item item);
        Item GetItem(int id);
        bool UpdateItem(Item item);
        List<Item> GetItems(int page, int pageSize);
    }
}
