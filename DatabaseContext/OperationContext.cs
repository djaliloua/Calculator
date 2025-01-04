using CalculatorModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseContext
{
    public class OperationContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<OperationContext>()
                .Build()
                ;
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("Calculator_db2"));
            }
        }
        
    }
}
