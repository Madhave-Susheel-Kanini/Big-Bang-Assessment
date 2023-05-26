using System.ComponentModel.DataAnnotations;

namespace AssessmentAPI.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int RoomCount { get; set; }
        public string? RoomCreatedAt { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
