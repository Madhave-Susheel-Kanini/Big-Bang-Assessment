using AssessmentAPI.Model;
using AssessmentAPI.Models;
using AssessmentAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories
{
    public class BookingRepository : IBooking
    {
        private readonly HotelContext _context;

        public BookingRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetBooking()
        {
            return _context.Bookings.ToList();
        }

        public Booking GetBookingById(int BookingId)
        {
            return _context.Bookings.Find(BookingId);
        }

        public Booking PostBooking(Booking booking)
        {
            try
            {
                booking.BookedDate = DateTime.UtcNow.ToString();

                var hotel = _context.Hotels.Find(booking.Hotel.HotelId);
                if (hotel == null)
                {
                    throw new Exception("Invalid hotel ID.");
                }

                var customer = _context.Customers
                    .Include(c => c.Hotel)
                    .FirstOrDefault(c => c.CustomerId == booking.Customer.CustomerId && c.Hotel.HotelId == hotel.HotelId);
                if (customer == null)
                {
                    throw new Exception($"Customer is not associated with the specified hotel.");
                }

                var room = _context.Rooms
                    .Include(r => r.Hotel)
                    .FirstOrDefault(r => r.RoomId == booking.Room.RoomId && r.Hotel.HotelId == hotel.HotelId);
                if (room == null)
                {
                    throw new Exception("Room is not associated with the specified hotel.");
                }

                if (room.RoomCount > 0)
                {
                    room.RoomCount--;
                    _context.Entry(room).State = EntityState.Modified;
                    booking.Room = room;
                }
                else
                {
                    throw new Exception("No available rooms for booking.");
                }

                booking.Hotel = hotel;
                booking.Customer = customer;

                _context.Add(booking);
                _context.SaveChanges();

                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create booking.", ex);
            }
        }
        public Booking PutBooking(int BookingId, Booking booking)
        {
            var existingBooking = _context.Bookings.Find(BookingId);
            if (existingBooking != null)
            {
                existingBooking.CheckInDate = booking.CheckInDate;
                existingBooking.CheckOut = booking.CheckOut;
                existingBooking.Room = booking.Room;

                _context.SaveChanges();
            }
            return existingBooking;
        }

        public Booking DeleteBooking(int BookingId)
        {
            try
            {
                var booking = _context.Bookings.Include(booking => booking.Room).FirstOrDefault(booking => booking.BookingId == BookingId);

                if (booking != null)
                {
                    var room = booking.Room;

                    if (room != null)
                    {
                        room.RoomCount++;
                        _context.Entry(room).State = EntityState.Modified;
                    }

                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();

                    return booking;
                }
                else
                {
                    throw new Exception("Booking not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete booking.", ex);
            }
        }
    }
}
