using SoulTrees.Enumerations;

namespace SoulTrees.Models
{
    public class Tree
    {
        public int Id { get; set; }
        public ETreeType Type { get; set; }
        public List<Skill> Skills { get; set; }
        public Tree() 
        {
            Skills = new List<Skill>();
        }
    }
}
