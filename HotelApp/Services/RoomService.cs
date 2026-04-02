using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class RoomService : ICrud
    {
        private readonly ApplicationDbContext _db;
        public RoomService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Create()
        {

            Console.WriteLine("Skapa ett nytt rum");

            var roomNameInput = VarValidater.GetRequiredString("Ange rummets namn: ");

            var roomSizeInput = VarValidater.GetRequiredInt("Ange rummets storlek i kvm: ");

            var roomPrizeInput = VarValidater.GetRequiredDecimal("Ange rummets pris: ");

            if (_db.Rooms.Any(r => r.RoomName == roomNameInput))
                Console.WriteLine("Rummet finns redan inlagt. Dubbelkika bland raderade rum om det har blivit inavktiverat");
            else
            {
                _db.Rooms.Add(new Room
                {
                    RoomName = roomNameInput,
                    Area = roomSizeInput,
                    Price = roomPrizeInput,
                    IsActive = true
                });
                _db.SaveChanges();
            }

        }

        public void Delete()
        {

            Console.Clear();
            var selectMessage = new Text($"Välj från listan med rum vilket du vill ta bort.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);

            Messages.WaitForKey();

            var activeRooms = _db.Rooms.Where(c => c.IsActive).ToList();
            if (!activeRooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga rum att radera.[/]");
                return;
            }

            var roomOptions = activeRooms.Select(c => $"{c.RoomId} {c.RoomName} ").ToList();

            int index = ScrollMenu.ShowScrollingMenu(roomOptions);

            var roomToRemove = activeRooms[index];
            roomToRemove.IsActive = false;
            _db.SaveChanges();


            var removedRoom = new Text($"{roomToRemove.RoomName} har tagits bort.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(removedRoom);
            AnsiConsole.WriteLine();

            Messages.WaitForKey();
            Console.Clear();

        }

        public void Reactivate()
        {

            Console.Clear();
            var selectMessage = new Text($"Välj från listan med inaktiva rum vilket du vill aktivera.", new Style(Color.Cyan))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);

            Messages.WaitForKey();

            var inactiveRooms = _db.Rooms.Where(c => !c.IsActive).ToList();
            if (!inactiveRooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga  inaktiva rum att aktivera.[/]");
                return;
            }

            var roomOptions = inactiveRooms.Select(c => $"{c.RoomId} {c.RoomName}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(roomOptions);

            var returningRoom = inactiveRooms[index];
            returningRoom.IsActive = true;
            _db.SaveChanges();


            var reactivatedCustomer = new Text($"{returningRoom.RoomName} har återaktiverats.", new Style(Color.Green))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(reactivatedCustomer);
            AnsiConsole.WriteLine();
            Messages.WaitForKey();
            Console.Clear();

        }

        public void Read()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var activeRooms = _db.Rooms.Where(r => r.IsActive).ToList();
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

        public void ReadDeleted()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();

            var deletedRooms = _db.Rooms.Where(r => !r.IsActive).ToList();

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

        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med rum vilket du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);

            Messages.WaitForKey();

            var rooms = _db.Rooms.Where(c => c.IsActive).ToList();

            if (!rooms.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga rum att visa.[/]");
                return;
            }

            var nameOptions = rooms.Select(c => $"Id: {c.RoomId} Namn: {c.RoomName}").ToList();

            int index = ScrollMenu.ShowScrollingMenu(nameOptions);

            var roomToUpgrade = rooms[index];
            var updateOptions = new List<string> { "Rummets namn", "Rummets storlek (kvm)", "Pris/natt" };

            int updateIndex = ScrollMenu.ShowScrollingMenu(updateOptions);

            switch (updateIndex)
            {
                case 0:
                    roomToUpgrade.RoomName = VarValidater.GetRequiredString("Nytt rumsnamn: ");
                    break;

                case 1:
                    roomToUpgrade.Area = VarValidater.GetRequiredInt("Ny storlek i kvm: ");
                    break;

                case 2:
                    roomToUpgrade.Price = VarValidater.GetRequiredDecimal("Nytt pris: ");
                    break;
            }

            _db.SaveChanges();

        }
    }
}
