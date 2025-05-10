using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelRepositories.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly RoomTypeDAO _roomTypeDao;

        public RoomTypeRepository(RoomTypeDAO roomTypeDao)
        {
            _roomTypeDao = roomTypeDao;
        }

        public Task<RoomType> AddRoomTypeAsync(RoomType roomType)
        {
            return _roomTypeDao.AddAsync(roomType);
        }

        public Task DeleteRoomTypeAsync(int id)
        {
            return _roomTypeDao.DeleteAsync(id);
        }

        public Task<List<RoomType>> GetAllRoomTypesAsync()
        {
            return _roomTypeDao.GetAllAsync();
        }

        public Task<RoomType> GetRoomTypeByIdAsync(int id)
        {
            return _roomTypeDao.GetByIdAsync(id);
        }

        public Task<List<RoomType>> GetRoomTypesByNameAsync(string name)
        {
            return _roomTypeDao.GetByNameAsync(name);
        }

        public Task<bool> RoomTypeExistsAsync(int id)
        {
            return _roomTypeDao.ExistsAsync(id);
        }

        public Task UpdateRoomTypeAsync(RoomType roomType)
        {
            return _roomTypeDao.UpdateAsync(roomType);
        }
    }
}