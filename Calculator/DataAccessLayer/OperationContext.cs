using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer
{
    public class OperationContext:DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=calculator.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasKey(x => x.Id);
        }
    }
}
