using SoulTrees.Models;
using SoulTrees.Repositories;

namespace SoulTrees.Managers
{
    public class ItemManager : IItemManager
    {
        private const string CLASS_NAME = "ItemManager";
        private readonly IItemRepository _itemRepository;
        private readonly Repositories.ILogger _logger;

        public ItemManager(IItemRepository itemRepository, Repositories.ILogger logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<bool> AddItem(Item item)
        {
            await _logger.LogEvent(CLASS_NAME, "AddItem", string.Format("With name {0} and description {1}", item.Name, item.Description));
            return _itemRepository.AddItem(item);
        }

        public async Task<Item> GetItem(int id)
        {
            await _logger.LogEvent(CLASS_NAME, "GetItem", id.ToString());
            return _itemRepository.GetItem(id);
        }

        public async Task<List<Item>> GetItems(int page, int pageSize)
        {
            await _logger.LogEvent(CLASS_NAME, "GetItems", string.Format("Called on page {0} with size {1}", page, pageSize));
            return _itemRepository.GetItems(page, pageSize);
        }

        public async Task<bool> UpdateItem(Item item)
        {
            await _logger.LogEvent(CLASS_NAME, "UpdateItem", string.Format("Called with id {0}", item.Id));
            return _itemRepository.UpdateItem(item);
        }
    }
}
