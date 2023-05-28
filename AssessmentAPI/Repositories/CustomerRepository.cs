using AssessmentAPI.Model;
using AssessmentAPI.Models;
using AssessmentAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly HotelContext _context;

        public CustomerRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomer()
        {
            try
            {
                return _context.Customers.Include(x => x.Hotel).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving customers: {ex.Message}");

                return Enumerable.Empty<Customer>();
            }
        }

        public Customer GetCustomerById(int CustomerId)
        {
            try
            {
                return _context.Customers.Find(CustomerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving customer with ID {CustomerId}: {ex.Message}");
                return null;
            }
        }

        public Customer PostCustomer(Customer customer)
        {
            try
            {
                var cus = _context.Hotels.Find(customer.Hotel.HotelId);
                customer.Hotel = cus;
                customer.CustomerCreatedAt = DateTime.UtcNow.ToString();
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding a new customer.");
                return null;
            }
        }

        public Customer PutCustomer(int CustomerId, Customer customer)
        {
            try
            {
                var cus = _context.Hotels.Find(customer.Hotel.HotelId);
                customer.Hotel = cus;
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating customer with ID {CustomerId}.");
                return null;
            }
        }

        public Customer DeleteCustomer(int CustomerId)
        {
            try
            {
                var customer = _context.Customers.Find(CustomerId);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                }
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting customer with ID {CustomerId}.");
                return null;
            }
        }

        public IEnumerable<Hotel> FilterHotel(string amenities)
        {
            try
            {
                var filteredHotels = _context.Hotels.AsQueryable();

                if (!string.IsNullOrEmpty(amenities))
                {
                    filteredHotels = filteredHotels.Where(h => h.HotelAmenities.Contains(amenities));
                }
                return filteredHotels.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while filtering hotels.");
                return new List<Hotel>();
            }
        }

        public int GetRoomCountByHotel(int RoomId, int HotelId)
        {
            try
            {
                var count = (from room in _context.Rooms
                             join hotel in _context.Hotels on room.Hotel.HotelId equals hotel.HotelId
                             where room.RoomId == RoomId && hotel.HotelId == HotelId
                             select room.RoomCount).FirstOrDefault();

                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get room count by RoomId and HotelId.", ex);
            }
        }
        public IEnumerable<Room> GetRoomsByPriceRange(int minPrice, int maxPrice)
        {
            try
            {
                var rooms = _context.Rooms
                    .Include(r => r.Hotel)
                    .Where(r => r.RoomPrice >= minPrice && r.RoomPrice <= maxPrice)
                    .ToList();

                return rooms;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve rooms by price range.", ex);
            }
        }
    }
}
