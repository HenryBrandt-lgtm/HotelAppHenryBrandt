using HotelApp.Data;
using HotelApp.Services;

namespace HotelApp.MenuData
{
    public class MenuOptions
    {
        public static List<string> MainMenu()
        {
            return new List<string>
            {
            "Skapa en ny Bokning",
            "Hantera Bokningar",
            "Hantera Gäster",
            "Hantera Rum",
            "Avsluta program"
            };
        }
        public static List<MenuItem> BookingMenu(ICrud bookingCrud, Action exitAction)
        {
            return new List<MenuItem>
            {
                new("Se alla bokningar", bookingCrud.Read),
                new("Uppdatera bokning", bookingCrud.Update),
                new("Ta bort bokning", bookingCrud.Delete),
                new("Se inaktiva bokningar", bookingCrud.ReadDeleted),
                new("Återaktivera bokning",bookingCrud.Reactivate),
                new("Go back to Main Menu", exitAction)
               
            };
        }
        public static List<MenuItem> CustomerMenu(ICrud customerCrud, Action exitAction)
        {
            return new List<MenuItem>
            {
                new ("Lägg till ny gäst", customerCrud.Create),
                new ("Se alla gäster", customerCrud.Read),
                new ("Uppdatera gäst", customerCrud.Update),
                new ("Radera gäst", customerCrud.Delete),
                new ("Se inaktiva gäster", customerCrud.ReadDeleted),
                new ("Återaktivera gäster", customerCrud.Reactivate),
                new ("Go back to Main Menu", exitAction)
            };
        }
        public static List<MenuItem> RoomMenu(ICrud roomCrud, Action exit)
        {
            return new List<MenuItem>
            {
                new("Lägg till nytt rum", roomCrud.Create),
                new("Se alla rum", roomCrud.Read),
                new("Uppdatera rum", roomCrud.Update),
                new("Radera rum", roomCrud.Delete),
                new("Se inaktiva rum", roomCrud.ReadDeleted),
                new("Aktivera inaktiva rum", roomCrud.Reactivate),
                new("Go back to Main Menu", exit)
            };
        }
    }
}

