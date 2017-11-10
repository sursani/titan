using Microsoft.EntityFrameworkCore;
using Titan.Entities;

namespace Titan.Contexts
{
    public class TitanContext : DbContext
    {
        public TitanContext(DbContextOptions<TitanContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}