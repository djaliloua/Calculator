using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistrationApplication.MVVM.Models;

namespace RegistrationApplication.DataAccessLayer.DataContext
{
    public class TrainerDataContext:DbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<PictureFile> Pictures { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public TrainerDataContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(App.Configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
