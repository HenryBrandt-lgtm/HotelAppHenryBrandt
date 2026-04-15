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
            SeedBookings(dbContext);
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
                    Email = "henry.brandt@example.com",
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
                    Email = "hanna.verlage@example.com",
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
                    Email = "alex.araujo@example.com",
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
                    Email = "mirza.hujic@example.com",
                    IsActive = true
                });
            }
        }
        private void SeedRooms(ApplicationDbContext dbContext)
        {
            if (!dbContext.Rooms.Any(r => r.RoomName == "Buffert OneBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Buffert OneBed",
                    Area = 15,
                    RoomType = Enums.RoomType.Single,
                    Beds = 1,
                    Price = 140,
                    IsActive= true
                });
            }
            if (!dbContext.Rooms.Any(r => r.RoomName == "Luxury OneBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Luxury OneBed",
                    Area = 28,
                    RoomType = Enums.RoomType.Single,
                    Beds = 1,
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
                    RoomType = Enums.RoomType.Double,
                    Beds = 2,
                    ExtraBeds = 1,
                    Price = 280,
                    IsActive= true
                });
            }
            if (!dbContext.Rooms.Any(r => r.RoomName == "Luxury TwoBed"))
            {
                dbContext.Rooms.Add(new Room
                {
                    RoomName = "Luxury TwoBed",
                    Area = 45,
                    RoomType = Enums.RoomType.Double,
                    Beds = 2,
                    ExtraBeds= 2,
                    Price = 350,
                    IsActive = true
                });
            }
        }
        private void SeedBookings(ApplicationDbContext dbContext)
        {
            if (!dbContext.Bookings.Any())
            {
                var customer1 = dbContext.Customers.First(c => c.FirstName == "Henry");
                var customer2 = dbContext.Customers.First(c => c.FirstName == "Hanna");
                var room1 = dbContext.Rooms.First(r => r.RoomName == "Buffert OneBed");
                var room2 = dbContext.Rooms.First(r => r.RoomName == "Luxury OneBed");

                var booking = new Booking
                {
                    StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
                    EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10)),
                    Customer = customer1,
                    Room = room1
                };

                booking.Invoice = new Invoice
                {
                    Amount = booking.TotalCost,
                    InvoiceStartDate = DateTime.Now,
                    IsPaid = true,
                    BookingId = booking.BookingId,
                    Booking = booking
                };

                var booking2 = new Booking
                {
                    StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
                    EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(12)),
                    Customer = customer2,
                    Room = room2
                };
                booking2.Invoice = new Invoice
                {
                    Amount = booking2.TotalCost,
                    InvoiceStartDate = DateTime.Now,
                    IsPaid = true,
                    BookingId = booking2.BookingId,
                    Booking = booking2
                };

                var booking3 = new Booking
                {
                    StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10)),
                    EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15)),
                    Customer = customer2,
                    Room = room1
                };
                booking3.Invoice = new Invoice
                {
                    Amount = booking3.TotalCost,
                    InvoiceStartDate = DateTime.Now,
                    IsPaid = true,
                    BookingId = booking3.BookingId,
                    Booking = booking3
                };

                dbContext.Bookings.Add(booking);
                dbContext.Bookings.Add(booking2);
                dbContext.Bookings.Add(booking3);
            }
        }
    }
}
