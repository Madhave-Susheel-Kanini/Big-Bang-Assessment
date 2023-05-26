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
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int CustomerId)
        {
            return _context.Customers.Find(CustomerId);
        }

        public Customer PostCustomer(Customer customer)
        {
            var cus = _context.Hotels.Find(customer.Hotel.HotelId);
            customer.Hotel = cus;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer PutCustomer(int CustomerId, Customer customer)
        {
            var cus = _context.Hotels.Find(customer.Hotel.HotelId);
            customer.Hotel = cus; ;
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return customer;
        }

        public Customer DeleteCustomer(int CustomerId)
        {
            var customer = _context.Customers.Find(CustomerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return customer;
        }
        public IEnumerable<Hotel> FilterHotel(string amenities)
        {
            var filteredHotels = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(amenities))
            {
                filteredHotels = filteredHotels.Where(h => h.HotelAmenities.Contains(amenities));
            }
            return filteredHotels.ToList();
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
    }
}
