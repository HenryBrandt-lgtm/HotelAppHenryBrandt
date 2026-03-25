using HotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet <Booking> Booking { get; set; } 

        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=HenrysHotel;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }
    }
}
