using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SoulTrees.Data;
using SoulTrees.Models;
using SoulTrees.Services;

namespace SoulTrees.Repositories
{
    public class Logger : ILogger
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public Logger(ApplicationDbContext context, UserManager<IdentityUser> userManager, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dateTimeProvider = dateTimeProvider;
            _configuration = configuration;
        }

        public async Task LogEvent(string className, string methodName, string? description)
        {
            if (!_configuration.GetSection("Logging").GetValue<bool>("IsEnabled"))
            {
                return;
            }

            IdentityUser? user = null;
            var userClaim = _httpContextAccessor.HttpContext?.User;
            if (userClaim != null) { user = await _userManager.GetUserAsync(userClaim); }

            string? ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (ip == null) { ip = "unknown address"; }

            var log = new Log()
            {
                Class = className,
                Method = methodName,
                Ip = ip,
                Details = description,
                UnixTime = _dateTimeProvider.GetUtcNow()
            };

            if (user != null)
                log.User = user;

            _context.Add(log);
            _context.SaveChangesResult();
        }
    }
}
