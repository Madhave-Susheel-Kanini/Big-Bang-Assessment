using AssessmentAPI.Model;
using AssessmentAPI.Models;
using AssessmentAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories
{
    public class RoomRepository : IRoom
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRoom()
        {
            return _context.Rooms.ToList();
        }

        public Room GetRoomById(int RoomId)
        {
            return _context.Rooms.Find(RoomId);
        }

        public Room PostRoom(Room room)
        {
            var r = _context.Hotels.Find(room.Hotel.HotelId);
            room.Hotel = r;
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return room;
        }

        public Room PutRoom(int RoomId, Room room)
        {
            var r = _context.Hotels.Find(room.Hotel.HotelId);
            room.Hotel = r;
            _context.Entry(room).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return room;
        }

        public Room DeleteRoom(int RoomId)
        {
            var room = _context.Rooms.Find(RoomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return room;
        }
    }
}
