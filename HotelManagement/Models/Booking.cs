using AssessmentAPI.Model;

namespace AssessmentAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? BookedDate { get; set; }
        public string? CheckInDate { get; set; }
        public string? CheckOut { get; set; }
        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }
        public Customer? Customer { get; set; }
    }
}
