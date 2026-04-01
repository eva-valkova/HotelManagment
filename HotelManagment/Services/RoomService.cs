using HotelManagment.Data; 
using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagment.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _db;

        public RoomService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _db.Rooms.ToList();
        }

        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();
        }

        public IEnumerable<Room> GetFilteredRooms(int? capacity, string type, bool? isFree)
        {
            var query = _db.Rooms.AsQueryable();

            if (capacity.HasValue)
                query = query.Where(r => r.Capacity == capacity);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(r => r.Type == type);

            if (isFree.HasValue)
                query = query.Where(r => r.IsFree == isFree);

            return query.ToList();
        }

        public void UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }

        public Room GetRoomById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime start, DateTime end, int? capacity = null)
        {
            var occupiedRoomIds = _db.Reservations
                .Where(res => start < res.CheckOutDate && end > res.CheckInDate)
                .Select(res => res.RoomId)
                .Distinct()
                .ToList();

            var query = _db.Rooms.Where(r => !occupiedRoomIds.Contains(r.Id));

            if (capacity.HasValue)
            {
                query = query.Where(r => r.Capacity >= capacity.Value);
            }

            return query.ToList();
        }

        
    }
}