using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }

        public Booking Booking { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate => InvoiceDate.AddDays(10);

        public bool IsPaid { get; set; }
    }
}
