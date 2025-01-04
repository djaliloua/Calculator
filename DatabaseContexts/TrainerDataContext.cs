using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Registration;

namespace DatabaseContexts
{
    public class TrainerDataContext : DbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<PictureFile> Pictures { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTimeTable> CourseTimeTables { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Participiant> Participiants { get; set; }
        public TrainerDataContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<TrainerDataContext>()
                .Build()
                ;
                optionsBuilder
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure()
                );
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
