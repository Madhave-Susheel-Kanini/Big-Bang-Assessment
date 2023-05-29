using System;
using System.IO;
using AssessmentAPI.Model;
using AssessmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Repositories
{
    public class HotelRepository : IHotel
    {
        private readonly HotelContext _context;
        private readonly string logFilePath = "error.log"; 

        public HotelRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Hotel> GetHotel()
        {
            try
            {
                return _context.Hotels.Include(x => x.Rooms).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("An error occurred while retrieving hotels. Please see the log file for details.");
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
                LogException(ex);
                throw new Exception($"An error occurred while retrieving hotel with ID {HotelId}. Please see the log file for details.");
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
                LogException(ex);
                throw new Exception("An error occurred while adding a new hotel. Please see the log file for details.");
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
                LogException(ex);
                throw new Exception($"An error occurred while updating hotel with ID {HotelId}. Please see the log file for details.");
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
                LogException(ex);
                throw new Exception($"An error occurred while deleting hotel with ID {HotelId}. Please see the log file for details.");
            }
        }

        private void LogException(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"Exception occurred at {DateTime.UtcNow}:");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine();
                }
            }
            catch
            {
                throw new Exception($"An error occurred while creating log.");
            }
        }
    }
}
