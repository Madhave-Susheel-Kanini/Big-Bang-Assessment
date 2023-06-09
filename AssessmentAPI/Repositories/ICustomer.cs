﻿using AssessmentAPI.Model;

namespace AssessmentAPI.Repositories
{
    public interface ICustomer
    {
        public IEnumerable<Customer> GetCustomer();
        public Customer GetCustomerById(int CustomerId);
        public Customer PostCustomer(Customer customer);
        public Customer PutCustomer(int CustomerId, Customer customer);
        public Customer DeleteCustomer(int CustomerId);
        public IEnumerable<Hotel> FilterHotel(string amenities);
        public string GetRoomCountByHotel(int RoomId, int HotelId);
        public IEnumerable<Room> GetRoomsByPriceRange(int minPrice, int maxPrice);

    }
}
