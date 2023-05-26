using AssessmentAPI.Model;

namespace AssessmentAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOut { get; set; }
        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }
        public Customer? Customer { get; set; }
        public Booking()
        {
            BookedDate = DateTime.Now;
        }
    }
}
