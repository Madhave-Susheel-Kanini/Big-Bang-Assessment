using Microsoft.AspNetCore.Mvc;
using AssessmentAPI.Model;
using AssessmentAPI.Repositories;

namespace AssessmentAPI.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotel _hotelRepository;

        public HotelController(IHotel hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _hotelRepository.GetHotel();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult PostHotel(Hotel hotel)
        {
            var newHotel = _hotelRepository.PostHotel(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = newHotel.HotelId }, newHotel);
        }

        [HttpPut("{id}")]
        public IActionResult PutHotel(int id, Hotel hotel)
        {
            var updatedHotel = _hotelRepository.PutHotel(id, hotel);
            if (updatedHotel == null)
                return NotFound();

            return Ok(updatedHotel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var deletedHotel = _hotelRepository.DeleteHotel(id);
            if (deletedHotel == null)
                return NotFound();

            return Ok(deletedHotel);
        }
    }
}