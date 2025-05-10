using HotelBusiness.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelRepositories.IRepository
{
    public interface IRoomTypeRepository
    {
        Task<List<RoomType>> GetAllRoomTypesAsync();
        Task<RoomType> GetRoomTypeByIdAsync(int id);
        Task<RoomType> AddRoomTypeAsync(RoomType roomType);
        Task UpdateRoomTypeAsync(RoomType roomType);
        Task DeleteRoomTypeAsync(int id);
        Task<bool> RoomTypeExistsAsync(int id);
        Task<List<RoomType>> GetRoomTypesByNameAsync(string name);
    }
}