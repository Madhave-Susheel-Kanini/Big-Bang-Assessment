using AssessmentAPI.Model;
using AssessmentAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       private readonly ICustomer _customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomer();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult PostCustomer(Customer customer)
        {
            var newCustomer = _customerRepository.PostCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.CustomerId }, newCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, Customer customer)
        {
            var updatedCustomer = _customerRepository.PutCustomer(id, customer);
            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var deletedCustomer = _customerRepository.DeleteCustomer(id);
            if (deletedCustomer == null)
                return NotFound();

            return Ok(deletedCustomer);
        }

        [HttpGet("filter")]
        public IEnumerable<Hotel> FilterHotel(string amenities)
        {
            return _customerRepository.FilterHotel(amenities);
        }

        [HttpGet("roomcount")]
        public int GetRoomCountByRoomIdAndHotelId(int RoomId, int HotelId)
        {
            return _customerRepository.GetRoomCountByHotel(RoomId, HotelId);
        }
    }
}
