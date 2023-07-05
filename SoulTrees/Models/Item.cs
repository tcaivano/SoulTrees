using SoulTrees.Enumerations;

namespace SoulTrees.Models
{
    public class Item
    {
        public int Id { get; set; }
        public EItemType Type { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Tree> BaseTrees { get; set; }
        public List<Tree> LowTrees { get; set; }
        public List<Tree> MidTrees { get; set; }
        public List<Tree> HighTrees { get; set; }

        public Item() 
        {
            BaseTrees = new List<Tree>();
            LowTrees = new List<Tree>();
            MidTrees = new List<Tree>();
            HighTrees = new List<Tree>();
        }

        static public void OrderTree(ref Tree tree)
        {
            tree.Skills = tree.Skills.OrderBy(x => x.Tier).ToList();
        }
    }
}
