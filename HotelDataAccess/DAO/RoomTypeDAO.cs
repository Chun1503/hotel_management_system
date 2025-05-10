using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDataAccess.DAO
{
    public class RoomTypeDAO
    {
        private readonly HotelDbContext _context;

        public RoomTypeDAO(HotelDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RoomType> AddAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task DeleteAsync(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<RoomType>> GetAllAsync()
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .OrderBy(rt => rt.TypeName)
                .ToListAsync();
        }

        public async Task<RoomType> GetByIdAsync(int id)
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.IdroomType == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.RoomTypes
                .AnyAsync(rt => rt.IdroomType == id);
        }

        public async Task<List<RoomType>> GetByNameAsync(string name)
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .Where(rt => rt.TypeName.Contains(name))
                .OrderBy(rt => rt.TypeName)
                .ToListAsync();
        }

        public async Task UpdateAsync(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync();
        }
    }
}