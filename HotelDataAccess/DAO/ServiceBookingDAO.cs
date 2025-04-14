using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataAccess.DAO
{
    public class ServiceBookingDAO
    {
        private readonly HotelDbContext _context;

        public ServiceBookingDAO(HotelDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách dịch vụ của đơn đặt phòng
        public async Task<IEnumerable<ServiceBooking>> GetServiceBookingsByBookingIdAsync(int bookingId)
        {
            return await _context.ServiceBookings
                .Where(sb => sb.Idbooking == bookingId)
                .Include(sb => sb.IdserviceNavigation)
                .ToListAsync();
        }

        // Thêm dịch vụ vào đơn đặt phòng
        public async Task<bool> AddServiceToBookingAsync(ServiceBooking serviceBooking)
        {
            _context.ServiceBookings.Add(serviceBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa dịch vụ khỏi đơn đặt phòng
        public async Task<bool> RemoveServiceFromBookingAsync(int serviceBookingId)
        {
            var serviceBooking = await _context.ServiceBookings.FindAsync(serviceBookingId);
            if (serviceBooking == null) return false;

            _context.ServiceBookings.Remove(serviceBooking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
