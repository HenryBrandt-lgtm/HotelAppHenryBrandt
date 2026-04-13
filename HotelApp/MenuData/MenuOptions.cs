using HotelApp.Controllers;
using HotelApp.Enums;

namespace HotelApp.MenuData
{
    public class MenuOptions
    {
        public static List<string> MainMenu()
        {
            return new List<string>
            {
                "Se lediga rum",
                "Skapa en ny Bokning",
                "Betala bokning",
                "Hantera Bokningar",
                "Hantera Gäster",
                "Hantera Rum",
                "Avsluta program"
            };
        }
        public static List<RoomType> RoomTypeMenu()
        {
            return new List<RoomType>
            {
                RoomType.Single,
                RoomType.Double
            };
        }
        public static List<MenuItem> BookingMenu(BookingController controller, Action exitAction)
        {
            return new List<MenuItem>
            {
                new("Se alla bokningar", controller.Read),
                new("Uppdatera bokning", controller.Update),
                new("Ta bort bokning/avboka", controller.Delete),
                new("Gå tillbaka till huvudmenyn", exitAction)

            };
        }
        public static List<MenuItem> CustomerMenu(CustomerController controller, Action exitAction)
        {
            return new List<MenuItem>
            {
                new ("Lägg till ny gäst", controller.Create),
                new ("Se alla gäster", controller.Read),
                new ("Uppdatera gäst", controller.Update),
                new ("Radera gäst", controller.Delete),
                new ("Se inaktiva gäster", controller.ReadDeleted),
                new ("Återaktivera gäster", controller.Reactivate),
                new ("Gå tillbaka till huvudmenyn", exitAction)
            };
        }
        public static List<MenuItem> RoomMenu(RoomController controller, Action exit)
        {
            return new List<MenuItem>
            {
                new("Lägg till nytt rum", controller.CreateRoom),
                new("Se alla rum", controller.ReadRooms),
                new("Uppdatera rum", controller.UpdateRoom),
                new("Radera rum", controller.DeleteRoom),
                new("Se inaktiva rum", controller.ReadDeletedRooms),
                new("Aktivera inaktiva rum", controller.ReactivateRoom),
                new("Gå tillbaka till huvudmenyn", exit)
            };
        }

    }
}

