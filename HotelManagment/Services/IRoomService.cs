using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagment.Services
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAllRooms();

        IEnumerable<Room> GetFilteredRooms(int? capacity, string type, bool? isFree);

        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
        Room GetRoomById(int id);

        IEnumerable<Room> GetAvailableRooms(DateTime start, DateTime end, int? capacity = null);
        
    }
}