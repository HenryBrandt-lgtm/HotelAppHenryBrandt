namespace HotelApp.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }
        public int Area { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<Booking> Bookings { get; set; } = 
            new List<Booking>();

        public int Beds
        {
            get
            {
                if (Area >= 30)
                    return 2;                
                else return 1;
            }
        }

        public int ExtraBeds
        {
            get
            {
                if (Area >= 40)
                    return 2;
                else if (Area > 20)
                    return 1;
                else return 0;
            }
        }
        public int TotalBeds => Beds + ExtraBeds;

        public bool IsBooked
        {
            get
            {
                return Bookings.Any(b => b.IsBooked());
            }
        }
        public string RoomType
        {
            get
            {
                if (Area >= 30)
                    return "Dubbelrum";                
                else
                    return "Enkelrum";
            }
        }
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
