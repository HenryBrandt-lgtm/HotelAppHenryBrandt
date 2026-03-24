using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data
{
    public class Invoice
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate {  get; set; }
    }
}
