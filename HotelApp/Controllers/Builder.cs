using HotelApp.Data;
using HotelApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Controllers
{
    public class Builder
    {
        public static void Build()
        {

            //var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

            //var config = builder.Build();

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            //var connectionString = config.GetConnectionString("DefaultConnection");

            //options.UseSqlServer(connectionString);

            //using (var dbContext = new ApplicationDbContext(options.Options))
            //{
            //    var dataInitializer = new DataInitializer();
            //    dataInitializer.MigrateAndSeed(dbContext);

            //}
            var factory = new ApplicationContextFactory();
            var dbContext = factory.CreateDbContext([]);

            // Anpassad!
            // Migrate & Seed databasen!
            var dataInitiaizer = new DataInitializer();
            dataInitiaizer.MigrateAndSeed(dbContext);

        }
    }
}
