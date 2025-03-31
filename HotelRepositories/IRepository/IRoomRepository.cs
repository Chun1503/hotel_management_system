using HotelBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRepositories.IRepository
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<List<Room>> GetAllAvailableRoomsAsync();
        Task<List<Room>> GetAvailableRoomsAsync(DateOnly startDate, DateOnly endDate, int? roomTypeId = null);

    }
}
