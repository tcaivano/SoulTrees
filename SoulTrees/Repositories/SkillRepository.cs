using SoulTrees.Data;
using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SkillTemplate> GetSkillTemplates()
        {
            return _context.SkillTemplates.ToList();
        }

        public bool AddSkillTemplate(SkillTemplate skillTemplate)
        {
            _context.SkillTemplates.Add(skillTemplate);
            return _context.SaveChangesResult();
        }

        public List<Skill> GetSkills()
        {
            return _context.Skills.ToList();
        }

        public void RefreshSkills()
        {
            var skills = this.GetSkills();
            var skillTemplates = this.GetSkillTemplates();
            var skillsToAdd = new List<SkillTemplate>();

            foreach (var skill in skills)
            {
                if (!skillTemplates.Where(x => x.Name == skill.Name && x.Description == skill.Description).Any() && !skillsToAdd.Where(x => x.Name == skill.Name && x.Description == skill.Description).Any())
                {
                    var newSkill = new SkillTemplate() { Description = skill.Description, Name = skill.Name };
                    skillsToAdd.Add(newSkill);
                    this.AddSkillTemplate(newSkill);
                }
            }
        }
    }
}
