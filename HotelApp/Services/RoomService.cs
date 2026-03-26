using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Services
{
    public class RoomService : ICrud
    {
        public void Create()
        {
            throw new NotImplementedException();
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
                var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;

                var activeRooms = dbContext.Room.Where(c => c.IsActive).ToList();

                var roomOptions = activeRooms.Select(c => $"{c.RoomId} {c.RoomName} ").ToList();

                int index = ScrollMenu.ScrollingMenu(roomOptions);

                var roomToRemove = activeRooms[index];
                roomToRemove.IsActive = false;
                dbContext.SaveChanges();


                var removedRoom = new Text($"{roomToRemove.RoomName} has been removed.", new Style(Color.Red))
                {
                    Justification = Justify.Center
                };
                AnsiConsole.Write(removedRoom);
                AnsiConsole.WriteLine();
                AnsiConsole.Write(pressAnyKeyMessage);

                Console.CursorVisible = false;
                Console.ReadKey(true);
                Console.CursorVisible = true;
                Console.Clear();
            }
        }

        public void Reactivate()
        {
            throw new NotImplementedException();
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

            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
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

            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }

        public void Update()
        {
            Console.Clear();
            var selectMessage = new Text($"Välj från listan med rum vilket du vill uppdatera.", new Style(Color.Red))
            {
                Justification = Justify.Center
            };
            AnsiConsole.Write(selectMessage);
            var pressAnyKeyMessage = Messages.GetPressAnyKeyText();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(pressAnyKeyMessage);

            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            using (var dbContext = new ApplicationDbContext())
            {
                var activeCustomers = dbContext.Room.Where(c => c.IsActive).ToList();

                var nameOptions = activeCustomers.Select(c => $"Id: {c.RoomId} Namn: {c.RoomName}").ToList();

                int index = ScrollMenu.ScrollingMenu(nameOptions);

                var roomToUpgrade = activeCustomers[index];
                var updateOptions = new List<string> { "FirstName", "LastName", "Birthday" };

                int updateIndex = ScrollMenu.ScrollingMenu(updateOptions);

                switch (updateIndex)
                {
                    case 0:
                        roomToUpgrade.RoomName = VarValidater.GetRequiredString("Nytt rumsnamn: ");
                        break;

                    case 1:
                        roomToUpgrade.Area = VarValidater.GetRequierdInt("Ny storlek: ");
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
