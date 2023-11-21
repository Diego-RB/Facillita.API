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

        internal object TotalIncome(int year, int month)
        {
            throw new NotImplementedException();
        }
    }
}
