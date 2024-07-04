using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer.Contexts
{
    public class OperationContext:DbContext
    {
        public OperationContext(DbContextOptions<OperationContext> options):base(options)
        {
            
        }
        public DbSet<Operation> Operations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasKey(x => x.Id);
        }
    }
}
