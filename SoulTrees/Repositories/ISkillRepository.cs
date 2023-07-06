using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public interface ISkillRepository
    {
        List<SkillTemplate> GetSkillTemplates();
        bool AddSkillTemplate(SkillTemplate skillTemplate);
        List<Skill> GetSkills();
        void RefreshSkills();
    }
}
