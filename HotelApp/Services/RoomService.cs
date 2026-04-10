using HotelApp.Data;
using HotelApp.Models;

namespace HotelApp.Services
{
    public class RoomService(ApplicationDbContext db)
    {
        public List<Room> GetActiveRooms()
        {
            return db.Rooms.Where(r => r.IsActive).ToList();
        }
        public List<Room> GetInactiveRooms()
        {
            return db.Rooms.Where(r => !r.IsActive).ToList();
        }
        public Room CreateRoom(string name, int area, int beds, decimal price)
        {
            if (db.Rooms.Any(r => r.RoomName == name))
                return null;

            var room = new Room
            {
                RoomName = name,
                Area = area,
                Beds = beds,
                Price = price,
                IsActive = true
            };

            db.Rooms.Add(room);
            db.SaveChanges();

            return room;
        }

        public void Delete(Room room)
        {
            room.IsActive = false;
            db.SaveChanges();
        }
        public void Reactivate(Room room)
        {
            room.IsActive = true;
            db.SaveChanges();
        }

        public void Update(Room room, string? name = null, int? area = null, int? beds = null, decimal? price = null)
        {
            if (name != null) room.RoomName = name;
            if (area != null) room.Area = area.Value;
            if (beds != null) room.Beds = beds.Value;
            if (price != null) room.Price = price.Value;

            db.SaveChanges();
        }
    }
}
