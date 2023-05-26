using AssessmentAPI.Model;
using AssessmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Repositories
{
    public class HotelRepository:IHotel
    {
        private readonly HotelContext _context;

        public HotelRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Hotel> GetHotel()
        {
            try
            {
                return _context.Hotels.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving hotels.");
                return new List<Hotel>();
            }
        }

        public Hotel GetHotelById(int HotelId)
        {
            try
            {
                return _context.Hotels.Find(HotelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving hotel with ID {HotelId}.");
                return null;
            }
        }

        public Hotel PostHotel(Hotel hotel)
        {
            try
            {
                hotel.HotelCreatedAt = DateTime.UtcNow.ToString();
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                return hotel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding a new hotel.");
                return null;
            }
        }

        public Hotel PutHotel(int HotelId, Hotel hotel)
        {
            try
            {
                var existingHotel = _context.Hotels.Find(HotelId);
                if (existingHotel != null)
                {
                    existingHotel.HotelName = hotel.HotelName;
                    existingHotel.HotelDescription = hotel.HotelDescription;
                    existingHotel.HotelLocation = hotel.HotelLocation;
                    _context.SaveChanges();
                }
                return existingHotel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating hotel with ID {HotelId}.");
                return null;
            }
        }

        public Hotel DeleteHotel(int HotelId)
        {
            try
            {
                var hotel = _context.Hotels.Find(HotelId);
                if (hotel != null)
                {
                    _context.Hotels.Remove(hotel);
                    _context.SaveChanges();
                }
                return hotel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting hotel with ID {HotelId}.");
                return null;
            }
        }
    }
}
