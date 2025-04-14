using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataAccess.DAO
{
    public class ServiceDAO
    {
        private readonly HotelDbContext _context;

        public ServiceDAO(HotelDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách dịch vụ
        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services
                .Where(s => s.Status == "Available")
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services.FindAsync(serviceId);
        }

        // Thêm dịch vụ mới
        public async Task<bool> CreateServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return true;
        }

        // Cập nhật dịch vụ
        public async Task<bool> UpdateServiceAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa dịch vụ
        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null) return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Service>> GetServicesByIdsAsync1(List<int> serviceIds)
        {
            return await _context.Services
                .Where(s => serviceIds.Contains(s.Idservice))
                .ToListAsync();
        }

    }
}
