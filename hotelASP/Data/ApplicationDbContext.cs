﻿using hotelASP.Entities;
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
            modelBuilder.Entity<UserAccount>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Room { get; set; } 
        public DbSet<RoomType> Types { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
