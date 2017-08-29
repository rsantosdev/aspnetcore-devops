using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Api.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(c => c.Name).IsRequired();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}