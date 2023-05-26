using AssessmentAPI.Model;
using AssessmentAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _roomRepository;

        public RoomController(IRoom roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomRepository.GetRoom();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public IActionResult PostRoom(Room room)
        {
            var newRoom = _roomRepository.PostRoom(room);
            room.RoomCreatedAt = DateTime.UtcNow.ToString();
            return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.RoomId }, newRoom);
        }

        [HttpPut("{id}")]
        public IActionResult PutRoom(int id, Room room)
        {
            var updatedRoom = _roomRepository.PutRoom(id, room);
            if (updatedRoom == null)
                return NotFound();

            return Ok(updatedRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var deletedRoom = _roomRepository.DeleteRoom(id);
            if (deletedRoom == null)
                return NotFound();

            return Ok(deletedRoom);
        }
    }
}
