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
        public List<Room> SetAvailableRooms(DateOnly startDate, DateOnly endDate, int numberOfGuests)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var rooms = dbContext.Room.Where(r => r.IsActive).Include(r => r.Bookings).ToList()
                    .Where(r => r.IsAvailable(startDate, endDate) && r.TotalBeds >= numberOfGuests)
                    .ToList();

                return rooms;
            }
        }
        public Room SelectRoom(List<Room> availableRooms, DateOnly startDate, DateOnly endDate)
        {
            var roomPanels = availableRooms.Select(r =>
            {
                var totalCost = r.GetTotalPrice(startDate, endDate);

                return new Panel(
                    $"[yellow]Rum:[/] {r.RoomName}  [blue]Typ:[/] {r.RoomType}  " +
                    $"[yellow]Pris/natt:[/] {r.Price:C} [blue]TotalPris:[/] {totalCost} " +
                    $"[yellow]Antal sängar:[/] {r.Beds} [blue]Antal extrasängar:[/] {r.ExtraBeds}")
                    .Border(BoxBorder.Rounded)
                    .Padding(1, 1)
                    .Header($"{r.RoomId}: {r.RoomName}")
                    .Expand();
            }).ToList();

            AnsiConsole.MarkupLine("[bold]Lediga rum:[/]");
            AnsiConsole.Write(new Columns(roomPanels));
            Room selectedRoom = null;
            while (true)
            {
                int selectedRoomId = VarValidater.GetRequiredInt("Ange rummets ID i listan: ");

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
