using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer.Contexts
{
    public class OperationContext:DbContext
    {
        public OperationContext(DbContextOptions<OperationContext> options):base(options)
        {
            
        }
        public OperationContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = calculator.db");
            //optionsBuilder.UseLazyLoadingProxies();
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Operation> Operations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasKey(x => x.Id);
        }
    }
}
