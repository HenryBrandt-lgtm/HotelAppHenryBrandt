using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(25)]
        public required string FirstName { get; set; }

        [StringLength(25)]
        public required string LastName { get; set; }

        public DateOnly Birthday { get; set; }

        [StringLength(50)]
        public required string Email { get; set; }

        public bool IsActive { get; set; }

        public List<Booking>? Bookings { get; set; }

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
