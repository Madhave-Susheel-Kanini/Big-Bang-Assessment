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
            var b = _context.Hotels.Find(booking.Hotel.HotelId);
            booking.Hotel = b;
            var room = _context.Rooms.Find(booking.Room.RoomId);
            booking.Room = room;

            var customer = _context.Customers.Find(booking.Customer.CustomerId);
            booking.Customer = customer;

            _context.Add(booking);
            _context.SaveChanges();
            return booking;
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
            var booking = _context.Bookings.Find(BookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
            return booking;
        }
    }
}
