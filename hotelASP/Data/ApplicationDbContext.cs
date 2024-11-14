using hotelASP.Entities;
using hotelASP.Models;
using Microsoft.EntityFrameworkCore;

namespace hotelASP.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginViewModel>().HasNoKey();
			modelBuilder.Entity<RegistrationViewModel>().HasNoKey();

			base.OnModelCreating(modelBuilder);
        }

      


        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Room> Room { get; set; } 
        public DbSet<hotelASP.Models.LoginViewModel> LoginViewModel { get; set; } = default!;
    }
}
