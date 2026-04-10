using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelApp.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }

        public required int BookingId { get; set; }
        public required Booking Booking { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvoiceStartDate { get; set; }
        public DateTime DueDate => InvoiceStartDate.AddDays(10);

        public bool IsPaid { get; set; }
    }
}
