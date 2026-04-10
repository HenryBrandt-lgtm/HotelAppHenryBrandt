using HotelApp.Enums;
using static HotelApp.Enums.RoomEnums;

namespace HotelApp.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public required string RoomName { get; set; }
        public required int Area { get; set; }
        public required decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<Booking>? Bookings { get; set; }

        public RoomType RoomType { get; set; }

        public int Beds { get; set; }

        public int ExtraBeds { get; set; }
        public int TotalBeds => Beds + ExtraBeds;

        public bool IsBooked => Bookings.Any(b => b.IsBooked());

        //public string RoomType
        //{
        //    get
        //    {
        //        if (Area >= 30)
        //            return "Dubbelrum";                
        //        else
        //            return "Enkelrum";
        //    }
        //}
        public bool IsAvailable(DateOnly start, DateOnly end, int? excludeBookingId = null)
        {
            return !Bookings
                .Where(b => excludeBookingId == null || b.BookingId != excludeBookingId)
                .Any(b => b.IsActive && start <= b.EndDate && end >= b.StartDate);
        }
        public decimal GetTotalPrice(DateOnly start, DateOnly end)
        {
            int nights = (end.DayNumber - start.DayNumber) + 1;
            return nights * Price;
        }

    }
}
