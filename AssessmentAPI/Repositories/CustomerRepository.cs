using AssessmentAPI.Model;
using AssessmentAPI.Models;
using AssessmentAPI.Repositories;
using ClosedXML.Excel;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly HotelContext _context;
        private readonly string logFilePath = "success.xlsx";

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

        public string GetRoomCountByHotel(int RoomId, int HotelId)
        {
            try
            {
                var count = (from room in _context.Rooms
                             join hotel in _context.Hotels on room.Hotel.HotelId equals hotel.HotelId
                             where room.RoomId == RoomId && hotel.HotelId == HotelId
                             select room.RoomCount).FirstOrDefault();

                return "Rooms Available are : " + count;
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

                LogMessage("Rooms Available:", rooms);
                return rooms;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve rooms by price range.", ex);
            }
        }

        private void LogMessage(string header, IEnumerable<Room> rooms)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Log");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = header;
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    currentRow++;

                    foreach (var room in rooms)
                    {
                        worksheet.Cell(currentRow, 1).Value = "Room ID:";
                        worksheet.Cell(currentRow, 2).Value = room.RoomId;
                        worksheet.Cell(currentRow + 1, 1).Value = "Hotel ID:";
                        worksheet.Cell(currentRow + 1, 2).Value = room.Hotel?.HotelId.ToString() ?? "N/A";
                        worksheet.Cell(currentRow + 2, 1).Value = "Price:";
                        worksheet.Cell(currentRow + 2, 2).Value = room.RoomPrice.ToString() ?? "N/A";

                        currentRow += 4;
                    }

                    workbook.SaveAs(logFilePath);
                }
            }
            catch
            {
                throw new Exception("Cannot create log");
            }
        }
    }
}
