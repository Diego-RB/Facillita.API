using Facillita.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Facillita.API.Data
{
    public class FinancialContext : DbContext
    {
        public FinancialContext(DbContextOptions<FinancialContext> opt) : base(opt)
        {
        }
        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Extract> Extracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Extract>().HasNoKey().ToView("vw_extract");
        }
    }
}
