namespace HotelApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CostumerId { get; set; }

        public Customer Customer { get; set; }

        public bool IsActive { get; set; }
        public bool IsBooked()
        {
            return DateTime.Now >= StartDate && DateTime.Now <= EndDate;
        }
    }
}
