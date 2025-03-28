using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRepositories.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDAO _roomDAO;
        public RoomRepository(RoomDAO roomDAO)
        {
            _roomDAO = roomDAO;
        }


        public Task AddRoomAsync(Room room)
        {
           return _roomDAO.AddRoomAsync(room);
        }

        public Task DeleteRoomAsync(int id)
        {
            return _roomDAO.DeleteRoomAsync(id);
        }

        public Task<List<Room>> GetAllAvailableRoomsAsync()
        {
            return _roomDAO.GetAllAvailableRoomsAsync();
        }

        public Task<List<Room>> GetAllRoomsAsync()
        {
            return _roomDAO.GetAllRoomsAsync();
        }

        public Task<List<Room>> GetAvailableRoomsAsync(DateOnly startDate, DateOnly endDate, int? roomTypeId = null)
        {
            return _roomDAO.GetAvailableRoomsAsync(startDate, endDate, roomTypeId);
        }

        public Task<Room?> GetRoomByIdAsync(int id)
        {
            return _roomDAO.GetRoomByIdAsync(id);
        }

        public Task UpdateRoomAsync(Room room)
        {
            return _roomDAO.UpdateRoomAsync(room);
        }
    }
}
