using HotelApp.Data;

namespace HotelApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public bool IsActive
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                if (Invoice != null && !Invoice.IsPaid && DateTime.Now > Invoice.DueDate)
                {
                    return false;
                }

                return today <= EndDate;
            }
        }

        public int DurationDays => (EndDate.DayNumber - StartDate.DayNumber) + 1;

        public decimal TotalCost => Room != null ? DurationDays * Room.Price : 0m;

        public bool IsBooked()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return today >= StartDate && today <= EndDate;
        }
        public bool IsFuture
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                return today < StartDate;
            }
        }
    }
}
