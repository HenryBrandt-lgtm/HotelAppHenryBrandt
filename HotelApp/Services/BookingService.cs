using HotelApp.Data;
using HotelApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Services
{
    public class BookingService(ApplicationDbContext db)
    {
        public List<Room> SetAvailableRooms(DateOnly startDate, DateOnly endDate, int numberOfGuests)
        {
            var availableRooms = db.Rooms.Where(r => r.IsActive).Include(r => r.Bookings).ToList()
                .Where(r => r.IsAvailable(startDate, endDate) && r.TotalBeds >= numberOfGuests)
                .ToList();

            return availableRooms;
        }
        public List<Room> GetAvailableRoomsForBooking(DateOnly startDate, DateOnly endDate, int? excludeBookingId = null)
        {
            return db.Rooms.Include(r => r.Bookings).Where(r => r.IsActive).ToList()
                .Where(r => r.IsAvailable(startDate, endDate, excludeBookingId)).ToList();
        }
        public List<Booking> GetActiveBookings() => db.Bookings.Include(b => b.Customer)
            .Include(b => b.Room).Include(b => b.Invoice).ToList().Where(f => f.IsActive).ToList();

        public List<Booking> GetUnpaidBookings() => db.Bookings.Include(b => b.Customer).Include(b => b.Room).Include(b => b.Invoice).ToList()
                .Where(b => b.IsActive && b.Invoice != null && !b.Invoice.IsPaid).ToList();

        public void Create(Customer selectedCustomer, Room selectedRoom, DateOnly startDate, DateOnly endDate, decimal totalCost)
        {

            var newBooking = new Booking
            {
                CustomerId = selectedCustomer.CustomerId,
                RoomId = selectedRoom.RoomId,
                StartDate = startDate,
                EndDate = endDate,
                Room = selectedRoom,
                Customer = selectedCustomer
            };
            newBooking.Invoice = new Invoice
            {
                Amount = totalCost,
                InvoiceStartDate = DateTime.Now,
                IsPaid = false,
                Booking = newBooking,
                BookingId = newBooking.BookingId
            };

            db.Bookings.Add(newBooking);
            db.SaveChanges();
        }

        public void Delete(Booking bookingToRemove)
        {
            db.Bookings.Remove(bookingToRemove);
            db.SaveChanges();
        }

        public void Pay(Booking selectedBooking)
        {
            selectedBooking.Invoice.IsPaid = true;
            db.SaveChanges();
        }

        public void Update(Booking booking)
        {
            db.SaveChanges();
        }
    }
}