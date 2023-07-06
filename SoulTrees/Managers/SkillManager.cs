using SoulTrees.Models;
using SoulTrees.Repositories;

namespace SoulTrees.Managers
{
    public class SkillManager : ISkillManager
    {
        private readonly ISkillRepository _skillRepository;

        public SkillManager(ISkillRepository skillRepository) 
        {
            _skillRepository = skillRepository;
        }

        public List<SkillTemplate> GetSkillTemplates()
        {
            return _skillRepository.GetSkillTemplates();
        }
    }
}
