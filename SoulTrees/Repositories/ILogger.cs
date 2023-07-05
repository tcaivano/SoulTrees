using SoulTrees.Models;

namespace SoulTrees.Repositories
{
    public interface ILogger
    {
        Task LogEvent(string className, string methodName, string? description);
    }
}
