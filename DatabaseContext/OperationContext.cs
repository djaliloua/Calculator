using CalculatorModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseContext
{
    public class OperationContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public OperationContext(DbContextOptions<OperationContext> opions):base(opions) { }
    }
}
