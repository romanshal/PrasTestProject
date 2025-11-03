using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Entities;
using System;

namespace PrasTestProject.Data.Contexts
{
    public class NewsDbContext(DbContextOptions<NewsDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<News> News { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
