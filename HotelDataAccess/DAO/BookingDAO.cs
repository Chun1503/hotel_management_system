using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataAccess.DAO
{
    public class BookingDAO
    {
        private readonly HotelDbContext _context;

        public BookingDAO(HotelDbContext context)
        {
            _context = context;
        }

        // Thêm đơn đặt phòng
        public async Task<bool> CreateBookingAsync(Booking booking, List<int>? serviceIds)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            if (serviceIds != null && serviceIds.Count > 0)
            {
                var serviceBookings = serviceIds.Select(serviceId => new ServiceBooking
                {
                    Idbooking = booking.Idbooking,
                    Idservice = serviceId
                }).ToList();

                _context.ServiceBookings.AddRange(serviceBookings);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        // Lấy danh sách đặt phòng của người dùng
        public async Task<IEnumerable<Booking>> GetBookingsByAccountIdAsync(int accountId)
        {
            return await _context.Bookings
                .Where(b => b.Idaccount == accountId)
                .Include(b => b.IdroomNavigation)
                .Include(b => b.ServiceBookings)
                .ThenInclude(sb => sb.IdserviceNavigation)
                .ToListAsync();
        }

        // Lấy chi tiết một đơn đặt phòng
        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.IdroomNavigation)
                .Include(b => b.ServiceBookings)
                .ThenInclude(sb => sb.IdserviceNavigation)
                .FirstOrDefaultAsync(b => b.Idbooking == bookingId);
        }

        // Cập nhật đơn đặt phòng
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa đơn đặt phòng
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        // Cập nhật trạng thái đơn đặt phòng
        public async Task<bool> UpdateBookingStatusAsync(int bookingId, string status)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
