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
            if (!dbContext.Customer.Any(c => c.FirstName == "Henry" && c.LastName == "Brandt"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Henry",
                    LastName = "Brandt",
                    Birthday = new DateOnly(1993, 06, 07),
                    IsActive = true
                });
            }
            if (!dbContext.Customer.Any(c => c.FirstName == "Hanna" && c.LastName == "Verlage"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Hanna",
                    LastName = "Verlage",
                    Birthday = new DateOnly(1997, 03, 15),
                    IsActive = true
                });
            }
            if (!dbContext.Customer.Any(c => c.FirstName == "Alex" && c.LastName == "Araujo"))
            {
                dbContext.Customer.Add(new Customer
                {
                    FirstName = "Alex",
                    LastName = "Araujo",
                    Birthday = new DateOnly(1993, 03, 20),
                    IsActive = true
                });
            }
            if (!dbContext.Customer.Any(c => c.FirstName == "Mirza" && c.LastName == "Hujic"))
            {
                dbContext.Customer.Add(new Customer
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
            if (!dbContext.Room.Any(r => r.RoomName == "Luxury OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Luxury OneBed",
                    Area = 12,
                    Price = 200,
                    IsActive = true
                });
            }
            if (!dbContext.Room.Any(r => r.RoomName == "Regular OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Regular OneBed",
                    Area = 10,
                    Price = 150,
                    IsActive= true
                });
            }
            if (!dbContext.Room.Any(r => r.RoomName == "Buffert OneBed"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Buffert OneBed",
                    Area = 9,
                    Price = 140,
                    IsActive= true
                });
            }
            if (!dbContext.Room.Any(r => r.RoomName == "Family Room"))
            {
                dbContext.Room.Add(new Room
                {
                    RoomName = "Family Room",
                    Area = 20,
                    Price = 350,
                    IsActive = true
                });
            }
        }
    }
}
