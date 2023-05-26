using AssessmentAPI.Model;
using AssessmentAPI.Models;

namespace AssessmentAPI.Repositories
{
    public interface IBooking
    {
        public IEnumerable<Booking> GetBooking();
        public Booking GetBookingById(int BookingId);
        public Booking PostBooking(Room room);
        public Booking PutBooking(int BookingId, Booking booking);
        public Booking DeleteBooking(int BookingId);
    }
}
