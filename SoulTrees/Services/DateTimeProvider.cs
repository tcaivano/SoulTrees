namespace SoulTrees.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public long GetUtcNow()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
