using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.MenuData;
using HotelApp.Models;
using HotelApp.Services.CreateBookingServices;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HotelApp.Services.CreateBookingServices
{
    public class AvailableRooms
    {
        private readonly ApplicationDbContext _db;

        public AvailableRooms(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Room> SetAvailableRooms(DateOnly startDate, DateOnly endDate, int numberOfGuests)
        {

            var availableRooms = _db.Rooms.Where(r => r.IsActive).Include(r => r.Bookings).ToList()
                .Where(r => r.IsAvailable(startDate, endDate) && r.TotalBeds >= numberOfGuests)
                .ToList();

            return availableRooms;

        }
        public void ReadAvailableRooms(List<Room> availableRooms, DateOnly startDate, DateOnly endDate)
        {
            Console.Clear();
            var roomPanels = availableRooms.Select(r =>
            {
                var totalCost = r.GetTotalPrice(startDate, endDate);

                return new Panel(
                    $"[yellow]Rum:[/] {r.RoomName}  [blue]Typ:[/] {r.RoomType}  " +
                    $"[yellow]Storlek:[/] {r.Area}kvm " +
                    $"[blue]Pris/natt:[/] {r.Price:C} [yellow]TotalPris:[/] {totalCost} " +
                    $"[blue]Antal sängar:[/] {r.Beds} [yellow]Antal extrasängar:[/] {r.ExtraBeds}")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header($"{r.RoomId}: {r.RoomName}")
                    .Expand();
            }).ToList();
            AnsiConsole.MarkupLine("[bold]Lediga rum:[/]");
            AnsiConsole.Write(new Columns(roomPanels));
        }
        public Room SelectRoom(List<Room> availableRooms, DateOnly startDate, DateOnly endDate)
        {
            ReadAvailableRooms (availableRooms, startDate, endDate);
            Room selectedRoom = null;
            while (true)
            {
                int selectedRoomId = VarValidater.GetRequiredIntOverZero("Ange rummets ID i listan: ");

                selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == selectedRoomId);

                if (selectedRoom != null)
                {
                    return selectedRoom;
                }
                AnsiConsole.MarkupLine("[red]Ogiltigt ID försök igen.[/]");
            }
        }
    }
}
