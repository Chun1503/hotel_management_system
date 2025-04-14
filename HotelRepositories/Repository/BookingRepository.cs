using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;

namespace HotelRepositories.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDAO _bookingDAO;

        public BookingRepository(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public Task<bool> CreateBookingAsync(Booking booking, List<int>? serviceIds)
        {
            return _bookingDAO.CreateBookingAsync(booking, serviceIds);
        }

        public Task<IEnumerable<Booking>> GetBookingsByAccountIdAsync(int accountId)
        {
            return _bookingDAO.GetBookingsByAccountIdAsync(accountId);
        }

        public Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return _bookingDAO.GetBookingByIdAsync(bookingId);
        }

        public Task<bool> UpdateBookingAsync(Booking booking)
        {
            return _bookingDAO.UpdateBookingAsync(booking);
        }

        public Task<bool> DeleteBookingAsync(int bookingId)
        {
            return _bookingDAO.DeleteBookingAsync(bookingId);
        }

        public Task<bool> UpdateBookingStatusAsync(int bookingId, string status)
        {
            return _bookingDAO.UpdateBookingStatusAsync(bookingId, status);
        }
    }
}
