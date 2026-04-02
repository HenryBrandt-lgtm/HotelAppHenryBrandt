using HotelApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data
{
    public class DataInitializer
    {
        public void MigrateAndSeed(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
            SeedCustomers(dbContext);
            SeedRooms(dbContext);
            dbContext.SaveChanges();
        }
        private void SeedCustomers(ApplicationDbContext dbContext)
        {
            if (!dbContext.Customers.Any(c => c.FirstName == "Henry" && c.LastName == "Brandt"))
            {
                dbContext.Customers.Add(new Customer
                {
                    FirstName = "Henry",
                    LastName = "Brandt",
                    Birthday = new DateOnly(1993, 06, 07),
                    IsActive = true
                });
            }
            if (!dbContext.Customers.Any(c => c.FirstName == "Hanna" && c.LastName == "Verlage"))
            {
                dbContext.Customers.Add(new Customer
                {
                    FirstName = "Hanna",
                    LastName = "Verlage",
                    Birthday = new DateOnly(1997, 03, 15),
                    IsActive = true
                });
            }
            if (!dbContext.Customers.Any(c => c.FirstName == "Alex" && c.LastName == "Araujo"))
            {
                dbContext.Customers.Add(new Customer
                {
                    FirstName = "Alex",
                    LastName = "Araujo",
                    Birthday = new DateOnly(1993, 03, 20),
                    IsActive = true
                });
            }
            if (!dbContext.Customers.Any(c => c.FirstName == "Mirza" && c.LastName == "Hujic"))
            {
                dbContext.Customers.Add(new Customer
                {
                    FirstName = "Mirza",
                    LastName = "Hujic",
                    Birthday = new DateOnly(1992, 02, 26),
                    IsActive = true
                });
            }
        }
        private void SeedRooms(ApplicationDbContext dbContext)
        {
            if (!dbContext.Rooms.Any(r => r.RoomName == "Luxury OneBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Luxury OneBed",
                    Area = 28,
                    Price = 200,
                    IsActive = true
                });
            }
            if (!dbContext.Rooms.Any(r => r.RoomName == "Regular TwoBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Regular TwoBed",
                    Area = 35,
                    Price = 280,
                    IsActive= true
                });
            }
            if (!dbContext.Rooms.Any(r => r.RoomName == "Buffert OneBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Buffert OneBed",
                    Area = 15,
                    Price = 140,
                    IsActive= true
                });
            }
            if (!dbContext.Rooms.Any(r => r.RoomName == "Luxury TwoBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Luxury TwoBed",
                    Area = 45,
                    Price = 350,
                    IsActive = true
                });
            }
        }
    }
}
