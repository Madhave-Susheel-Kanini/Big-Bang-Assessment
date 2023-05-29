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
            try
            {
                return _context.Rooms.Include(x => x.Hotel).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving rooms.", ex);
            }
        }

        public Room GetRoomById(int RoomId)
        {
            try
            {
                return _context.Rooms.Find(RoomId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving room with ID {RoomId}.", ex);
            }
        }

        public Room PostRoom(Room room)
        {
            try
            {
                var r = _context.Hotels.Find(room.Hotel.HotelId);
                room.Hotel = r;
                room.RoomCreatedAt = DateTime.UtcNow.ToString();
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new room.", ex);
            }
        }

        public Room PutRoom(int RoomId, Room room)
        {
            try
            {
                var r = _context.Hotels.Find(room.Hotel.HotelId);
                room.Hotel = r;
                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating room with ID {RoomId}.", ex);
            }
        }

        public Room DeleteRoom(int RoomId)
        {
            try
            {
                var room = _context.Rooms.Find(RoomId);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    _context.SaveChanges();
                }
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting room with ID {RoomId}.", ex);
            }
        }

    }
}
