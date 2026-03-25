namespace HotelApp.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }
        public int Area { get; set; }
        public decimal Price { get; set; }
        public List<Booking> Bookings { get; set; } = 
            new List<Booking>();

        public int Beds
        {
            get
            {
                if (Area >= 20)
                    return 4;
                else if (Area >= 15)
                    return 2;
                else return 1;
            }
        }

        public int ExtraBeds
        {
            get
            {
                if (Area >= 20)
                    return 2;
                else if (Area > 15)
                    return 1;
                else return 0;
            }
        }

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
                if (Area >= 20)
                    return "multi room";
                else if (Area > 15)
                    return "double room";
                else
                    return "single room";
            }
        }

    }
}
