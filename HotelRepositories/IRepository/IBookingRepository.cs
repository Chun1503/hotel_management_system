using HotelBusiness.Models;

namespace HotelRepositories.IRepository
{
    public interface IBookingRepository
    {
        Task<bool> CreateBookingAsync(Booking booking, List<int>? serviceIds);
        Task<IEnumerable<Booking>> GetBookingsByAccountIdAsync(int accountId);
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<bool> UpdateBookingStatusAsync(int bookingId, string status);
    }
}
