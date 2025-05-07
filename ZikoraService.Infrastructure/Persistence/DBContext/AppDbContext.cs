using Microsoft.EntityFrameworkCore;
using ZikoraService.Domain.Entities;

namespace ZikoraService.Infrastructure.Persistence.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<CorporateAccount> CorporateAccount { get; set; }
        public DbSet<VirtualAccount> VirtualAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
