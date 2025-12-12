using Microsoft.EntityFrameworkCore;
using servweb1_t2.Domain.Entities;
using servweb1_t2.Infrastructure.Persistence.Configurations;

namespace servweb1_t2.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<servweb1_t2.Domain.Entities.ArticuloBaja> ArticulosBaja { get; set; }
        public DbSet<servweb1_t2.Domain.Entities.ArticuloLiquidacion> ArticulosLiquidacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}