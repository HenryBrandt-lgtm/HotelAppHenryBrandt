using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;

namespace HotelApp.Services
{
    public class RoomService : ICrud
    {
        public void Create()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.WriteLine("Skapa ett nytt rum");

                var roomNameInput = VarValidater.GetRequiredString("Ange rummets namn: ");

                var roomSizeInput = VarValidater.GetRequiredInt("Ange rummets storlek i kvm: ");

                var roomPrizeInput = VarValidater.GetRequiredDecimal("Ange rummets pris: ");


                //foreach (var county in dbContext.County)
                //{
                //    Console.WriteLine($"{county.Id} - {county.Name}");
                //}
                //Console.WriteLine("Ange Id på County");
                //var countyId = Convert.ToInt32(Console.ReadLine());
                //var countyInput = dbContext.County.First(c => c.Id == countyId);

                if (dbContext.Room.Any(r => r.RoomName == roomNameInput))
                    Console.WriteLine("Rummet finns redan inlagt. Dubbelkika bland raderade rum om det har blivit inavktiverat");
                else
                {
                    dbContext.Room.Add(new Room
                    {
                        RoomName = roomNameInput,
                        Area = roomSizeInput,
                        Price = roomPrizeInput,
                        IsActive = true
                    });
                    dbContext.SaveChanges();
                }
            }
        }

        public void Delete()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.Clear();
                var selectMessage = new Text($"Välj från listan med rum vilket du vill ta bort.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(selectMessage);

                CursorVisibility.WaitForKey();

                var activeRooms = dbContext.Room.Where(c => c.IsActive).ToList();
                if (!activeRooms.Any())
                {
                    AnsiConsole.MarkupLine("[red]Inga rum att radera.[/]");
                    return;
                }

                var roomOptions = activeRooms.Select(c => $"{c.RoomId} {c.RoomName} ").ToList();

                int index = ScrollMenu.ScrollingMenu(roomOptions);

                var roomToRemove = activeRooms[index];
                roomToRemove.IsActive = false;
                dbContext.SaveChanges();


                var removedRoom = new Text($"{roomToRemove.RoomName} har tagits bort.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(removedRoom);
                AnsiConsole.WriteLine();

                CursorVisibility.WaitForKey();
                Console.Clear();
            }
        }

        public void Reactivate()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Console.Clear();
                var selectMessage = new Text($"Välj från listan med inaktiva rum vilket du vill aktivera.", new Style(Color.Cyan))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(selectMessage);
                
                CursorVisibility.WaitForKey();

                var activeRooms = dbContext.Room.Where(c => !c.IsActive).ToList();

                var roomOptions = activeRooms.Select(c => $"{c.RoomId} {c.RoomName}").ToList();

                int index = ScrollMenu.ScrollingMenu(roomOptions);

                var returningRoom = activeRooms[index];
                returningRoom.IsActive = true;
                dbContext.SaveChanges();


                var reactivatedCustomer = new Text($"{returningRoom.RoomName} har återaktiverats.", new Style(Color.Green))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(reactivatedCustomer);
                AnsiConsole.WriteLine();
                CursorVisibility.WaitForKey();
                Console.Clear();
            }
        }

        public void Read()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            using (var dbContext = new ApplicationDbContext())
            {
                var activeRooms = dbContext.Room.Where(r => r.IsActive).ToList();
                if (!activeRooms.Any())
                {
                    AnsiConsole.WriteLine("Inga aktiva rum att visa.");
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
            }
            CursorVisibility.WaitForKey();
        }

        public void ReadDeleted()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            using (var dbContext = new ApplicationDbContext())
            {
                foreach (var room in dbContext.Room.Where(r => !r.IsActive))
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
            }           
            CursorVisibility.WaitForKey();
        }

        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med rum vilket du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            
            CursorVisibility.WaitForKey();

            using (var dbContext = new ApplicationDbContext())
            {
                var activeCustomers = dbContext.Room.Where(c => c.IsActive).ToList();

                var nameOptions = activeCustomers.Select(c => $"Id: {c.RoomId} Namn: {c.RoomName}").ToList();

                int index = ScrollMenu.ScrollingMenu(nameOptions);

                var roomToUpgrade = activeCustomers[index];
                var updateOptions = new List<string> { "Rummets namn", "Rummets storlek (kvm)", "Pris/natt" };

                int updateIndex = ScrollMenu.ScrollingMenu(updateOptions);

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

                dbContext.SaveChanges();
            }
        }
    }
}
