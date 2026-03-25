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
                "Remove Bookings",
                "Update Booking",
                "Go back to Main Menu"

            };
        }
        public static List<string> CustomerMenu()
        {
            return new List<string>
            {
                "View Customers",
                "Add new Customer",
                "Remove Customer",
                "Update Customer",
                "Go back to Main Menu"
            };
        }
    }
}

