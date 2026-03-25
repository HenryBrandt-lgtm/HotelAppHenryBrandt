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
            if (!dbContext.Customer.Any(c => c.FullName == "Henry Brandt"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Henry",
                    LastName = "Brandt",
                    Birthday = new DateTime(1993, 06, 07)
                });
            }
            if (!dbContext.Customer.Any(c => c.FullName == "Hanna Verlage"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Hanna",
                    LastName = "Verlage",
                    Birthday = new DateTime(1997, 03, 15)
                });
            }
            if (!dbContext.Customer.Any(c => c.FullName == "Alex Araujo"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Alex",
                    LastName = "Araujo",
                    Birthday = new DateTime(1993, 03, 20)
                });
            }
            if (!dbContext.Customer.Any(c => c.FullName == "Mirza Hujic"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Mirza",
                    LastName = "Hujic",
                    Birthday = new DateTime(1992, 02, 26)
                });
            }
        }
        private void SeedRooms(ApplicationDbContext dbContext)
        {
            if (!dbContext.Room.Any(c => c.RoomName == "Luxury OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Luxury OneBed",
                    Area = 12,
                    Price = 200,
                });
            }
            if (!dbContext.Room.Any(c => c.RoomName == "Regular OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Regular OneBed",
                    Area = 10,
                    Price = 150,
                });
            }
            if (!dbContext.Room.Any(c => c.RoomName == "Buffert OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Buffert OneBed",
                    Area = 9,
                    Price = 140,
                });
            }
            if (!dbContext.Room.Any(c => c.RoomName == "Luxury TwoBeds"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Family Room",
                    Area = 20,
                    Price = 350,
                });
            }
        }
    }
}
