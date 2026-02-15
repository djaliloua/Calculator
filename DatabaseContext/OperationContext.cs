using CalculatorModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext
{
    public class OperationContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public OperationContext(DbContextOptions<OperationContext> opions):base(opions) { }
    }
}
