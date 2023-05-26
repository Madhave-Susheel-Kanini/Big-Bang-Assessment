using System.ComponentModel.DataAnnotations;

namespace AssessmentAPI.Model
{
    public class Hotel
    {
        [Key] 
        public int HotelId { get; set; }
        public string? HotelName { get; set;}
         public string? HotelDescription { get; set; }
        public string? HotelLocation { get; set; }
        public string? HotelCreatedAt { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public ICollection<Customer>? Customers { get; set; }

    }
}
