using HotelApp.Data;
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
            throw new NotImplementedException();
        }

        public void Reactivate()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(HeaderDisplay.GetHeader());
            AnsiConsole.WriteLine();
            using (var dbContext = new ApplicationDbContext())
            {
                foreach (var room in dbContext.Room.Where(r => r.IsActive))
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
            throw new NotImplementedException();
        }
    }
}
