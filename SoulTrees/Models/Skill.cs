using SoulTrees.Enumerations;

namespace SoulTrees.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsPurchased { get; set; }
        
        public Skill() { }
    }
}
