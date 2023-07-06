using Microsoft.EntityFrameworkCore;
using SoulTrees.Data;
using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly  ISkillRepository _skillRepository;

        public ItemRepository(ApplicationDbContext context, ISkillRepository skillRepository)
        {
            _context = context;
            _skillRepository = skillRepository; 
        }

        public bool AddItem(Item item)
        {
            _context.Add(item);
            var result = _context.SaveChangesResult();
            _skillRepository.RefreshSkills();
            return result;
        }

        public bool UpdateItem(Item item)
        {
            _context.Update(item);
            var result = _context.SaveChangesResult();
            _skillRepository.RefreshSkills();
            return result;
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
