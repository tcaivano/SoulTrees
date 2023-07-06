using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoulTrees.Models;

namespace SoulTrees.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = "e977c9e2-f196-4ae1-9ae7-66f8af3b481e", ConcurrencyStamp = "17b4d97d-e6f5-47ed-bb25-279d5cc2e8fc" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "489c2c7c-f204-4fae-809f-1a9d1e540340", ConcurrencyStamp = "7399881c-3cff-4df4-b198-901989f0270d" });
        }

        public DbSet<Item> Items { get; set; } 
        public DbSet<Tree> Trees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SkillTemplate> SkillTemplates { get; set; }

        public bool SaveChangesResult()
        {
            return base.SaveChanges() > 0;
        }
    }
}