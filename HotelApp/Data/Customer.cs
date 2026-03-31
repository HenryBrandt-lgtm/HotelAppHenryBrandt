using HotelApp.Data;

namespace HotelApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateOnly Birthday { get; set; }

        public bool IsActive { get; set; }

        public List<Invoice> Invoices { get; set; } =
            new List<Invoice>();

        public List<Booking> Bookings { get; set; } =
            new List<Booking>();

        public string FullName => $"{FirstName} {LastName}";

        public int Age()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - Birthday.Year;
            if (Birthday > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
