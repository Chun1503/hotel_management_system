using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDataAccess.DAO
{
    public class RoomDAO
    {
        private readonly HotelDbContext _context;

        public RoomDAO(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.Include(r => r.IdroomTypeNavigation).ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.IdroomTypeNavigation)
                .FirstOrDefaultAsync(r => r.Idroom == id);
        }

        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(room.Idroom);
            if (existingRoom != null)
            {
                existingRoom.Name = room.Name;
                existingRoom.IdroomType = room.IdroomType;
                existingRoom.Price = room.Price;
                existingRoom.Image = room.Image;
                existingRoom.Status = room.Status;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Room>> GetAllAvailableRoomsAsync()
        {
            return await _context.Rooms
                .Where(r => r.Status == "Available")
                .Include(r => r.IdroomTypeNavigation)
                .ToListAsync();
        }

        public async Task<List<Room>> GetAvailableRoomsAsync(DateOnly startDate, DateOnly endDate, int? roomTypeId = null)
        {
            IQueryable<Room> query = _context.Rooms
                .Where(r => r.Status == "Available" &&
                            !_context.Bookings.Any(b =>
                                b.Idroom == r.Idroom &&
                                ((b.StartDate <= endDate && b.EndDate >= startDate) ||
                                 (b.StartDate >= startDate && b.StartDate <= endDate))))
                .Include(r => r.IdroomTypeNavigation);

            if (roomTypeId.HasValue)
            {
                query = query.Where(r => r.IdroomType == roomTypeId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
