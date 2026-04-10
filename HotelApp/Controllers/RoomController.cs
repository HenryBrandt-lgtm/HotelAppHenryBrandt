using HotelApp.Services;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Controllers
{
    public class RoomController(RoomService roomService)
    {
        public void CreateRoom()
        {
            Console.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());

            var name = VarValidater.GetRequiredString("Ange rummets namn: ");
            var area = VarValidater.GetRequiredIntOverZero("Ange rummets storlek i kvm: ");
            var beds = VarValidater.GetRequiredIntOverZero("Ange antal sängar: ");
            var price = VarValidater.GetRequiredDecimalOverZero("Ange pris/natt: ");

            var room = roomService.CreateRoom(name, area, beds, price);

            if (room == null)
                AnsiConsole.MarkupLine("[red]Rummet finns redan inlagt.[/]");
            else
                AnsiConsole.MarkupLine($"[green]Rummet '{room.RoomName}' har skapats![/]");

            Messages.WaitForKey();
        }
        public void DeleteRoom()
        {
            var activeRooms = roomService.GetActiveRooms();
            if (!activeRooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga rum att radera.[/]");
                Messages.WaitForKey();
                return;
            }

            var options = activeRooms.Select(r => r.RoomName).ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);
            var room = activeRooms[index];

            if (room.Bookings?.Any(b => b.IsActive) == true)
            {
                AnsiConsole.MarkupLine("[red]Rummet har aktiva bokningar och kan inte raderas.[/]");
                Messages.WaitForKey();
                return;
            }

            roomService.Delete(room);
            AnsiConsole.MarkupLine($"[green]Rummet '{room.RoomName}' har tagits bort.[/]");
            Messages.WaitForKey();
        }
        public void ReactivateRoom()
        {
            var rooms = roomService.GetInactiveRooms();
            if (!rooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga inaktiva rum.[/]");
                Messages.WaitForKey();
                return;
            }

            var options = rooms.Select(r => r.RoomName).ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);
            var room = rooms[index];

            roomService.Reactivate(room);
            AnsiConsole.MarkupLine($"[green]Rummet '{room.RoomName}' har återaktiverats.[/]");
            Messages.WaitForKey();
        }
        public void ReadRooms()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var activeRooms = roomService.GetActiveRooms();
            if (!activeRooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga aktiva rum att visa.[/]");
                return;
            }
            else
            {
                var panels = activeRooms.Select(room =>
                    new Panel(
                        $"[yellow]Rum:[/] {room.RoomName}\n" +
                        $"[blue]Rumstyp:[/] {room.RoomType}\n" +
                        $"[yellow]Sängar:[/] {room.Beds}\n" +
                        $"[blue]Extra sängar:[/] {room.ExtraBeds}\n" +
                        $"[yellow]Storlek:[/] {room.Area} m²\n" +
                        $"[blue]Pris:[/] {room.Price:C}\n" +
                        $"[green]Status:[/] {(room.IsBooked ? "[red]Upptaget[/]" : "Ledigt")}"
                    )
                    .Border(BoxBorder.Rounded)
                    .Header($"[bold]{room.RoomName}[/]")
                    .Padding(1, 1)
                    .Expand()
                ).ToList();

                var columns = new Columns(panels);
                AnsiConsole.Write(columns);
            }
            Messages.WaitForKey();
        }
        public void ReadDeletedRooms()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var deletedRooms = roomService.GetInactiveRooms();

            if (!deletedRooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga aktiva rum att visa.[/]");
                return;
            }

            foreach (var room in deletedRooms)
            {
                var content = new Markup($"[yellow]Rum:[/] {room.RoomName}" +
                    $"n [blue]Rumstyp:[/] {room.RoomType}" +
                    $"n [yellow]Antal sängar:[/] {room.Beds}" +
                    $"n [blue]Extra sängar:[/] {room.ExtraBeds}" +
                    $"\n [yellow]Storlek:[/] {room.Area}" +
                    $"\n[blue]Pris:[/] {room.Price}" +
                    $"\n [yellow]====================");

                AnsiConsole.Write(Align.Center(content));
            }

            Messages.WaitForKey();
        }
        public void UpdateRoom()
        {
            var roomToUpdate = roomService.GetActiveRooms();
            if (!roomToUpdate.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga rum att uppdatera.[/]");
                Messages.WaitForKey();
                return;
            }
            var options = roomToUpdate.Select(r => r.RoomName).ToList();
            int index = ScrollMenu.ShowScrollingMenu(options);
            var room = roomToUpdate[index];
            var updateOptions = new List<string> { "Rummets namn", "Storlek (kvm)", "Antal sängar", "Pris/natt" };
            int fieldIndex = ScrollMenu.ShowScrollingMenu(updateOptions);

            switch (fieldIndex)
            {
                case 0:
                    var newName = VarValidater.GetRequiredString("Nytt rumsnamn: ");
                    roomService.Update(room, name: newName);
                    break;
                case 1:
                    var newArea = VarValidater.GetRequiredIntOverZero("Ny storlek i kvm: ");
                    roomService.Update(room, area: newArea);
                    break;
                case 2:
                    var newBeds = VarValidater.GetRequiredIntOverZero("Nytt antal sängar: ");
                    roomService.Update(room, beds: newBeds);
                    break;
                case 3:
                    var newPrice = VarValidater.GetRequiredDecimalOverZero("Nytt pris/natt: ");
                    roomService.Update(room, price: newPrice);
                    break;
            }

            AnsiConsole.MarkupLine($"[green]Rummet '{room.RoomName}' har uppdaterats.[/]");
            Messages.WaitForKey();
        }
    }
}
