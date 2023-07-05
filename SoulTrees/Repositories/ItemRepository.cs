using Microsoft.EntityFrameworkCore;
using SoulTrees.Data;
using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddItem(Item item)
        {
            _context.Add(item);
            return _context.SaveChangesResult();
        }

        public bool UpdateItem(Item item)
        {
            _context.Update(item);
            return _context.SaveChangesResult();
        }

        public Item GetItem(int id)
        {
            return _context.Items.Where(x => x.Id == id)
                .Include(x => x.BaseTrees).ThenInclude(x => x.Skills)
                .Include(x => x.MidTrees).ThenInclude(x => x.Skills)
                .Include(x => x.LowTrees).ThenInclude(x => x.Skills)
                .Include(x => x.HighTrees).ThenInclude(x => x.Skills)
                .Single();
        }

        public List<Item> GetItems(int page, int pageSize)
        {
            return _context.Items.Skip(page * pageSize).Take(pageSize).ToList();
        }
    }
}
