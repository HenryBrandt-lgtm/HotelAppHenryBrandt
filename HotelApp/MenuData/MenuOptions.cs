namespace HotelApp.MenuData
{
    public class MenuOptions
    {
        public static List<string> MainMenu()
        {
            return new List<string>
            {
            "Book a Room",
            "View/remove/update Bookings",
            "Add/View/remove/update Customer",
            "Edit HotelRooms",
            "Terminate program"
            };
        }
        public static List<string> BookingMenu()
        {
            return new List<string>
            {
                "View Bookings",
                "Update Bookings",
                "Remove Booking",
                "Go back to Main Menu"

            };
        }
        public static List<string> CustomerMenu()
        {
            return new List<string>
            {
                "Create Customers",
                "Read Customers",
                "Update Customer",
                "Delete Customer",
                "Go back to Main Menu"
            };
        }
        public static List<string> RoomMenu()
        {
            return new List<string>
            {
                "Create Room",
                "Read Rooms",
                "Update Room",
                "Delete Room",
                "Go back to Main Menu"
            };
        }
    }
}

