using Microsoft.AspNetCore.Identity;

namespace SoulTrees.Models
{
    public class Log
    {
        public int Id { get; set; }
        public long UnixTime { get; set; }
        public IdentityUser? User { get; set; }
        public string? Ip { get; set; }
        public string? Method { get; set; }
        public string? Class { get; set; }
        public string? Details { get; set; }
    }
}
