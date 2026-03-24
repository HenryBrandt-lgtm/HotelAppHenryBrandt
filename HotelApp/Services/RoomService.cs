using System;
using System.Collections.Generic;
using System.Text;
using HotelApp.Models;

namespace HotelApp.Services
{
    public class RoomService
    {
        public List<Room> _Rooms = [];

        public void SeedRooms()
        {
            _Rooms.Add(new Room(1, 12, 200));
            _Rooms.Add(new Room(2, 10, 150));
            _Rooms.Add(new Room(3, 9, 140));
            _Rooms.Add(new Room(4, 20, 250));
        }
        public List<Room> GetRooms()
        {
            return _Rooms;
        }
    }
}
