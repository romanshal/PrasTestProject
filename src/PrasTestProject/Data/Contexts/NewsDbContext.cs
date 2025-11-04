using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Entities;
using System.Data;
using System.Reflection;

namespace PrasTestProject.Data.Contexts
{
    public class NewsDbContext(
        DbContextOptions<NewsDbContext> options) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<News> News { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser<Guid>>().ToTable("users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("users_tokens");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(NewsDbContext))!);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
